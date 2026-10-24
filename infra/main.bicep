// InsureAI - low-cost hosting for the Web API.
// Choices:
// - Linux App Service plan on the Free (F1) tier: no cost, suitable for dev/demo.
// - HTTPS only, TLS 1.2+, FTPS disabled, to keep the default posture secure.
// - Resource group scope, matching the Contributor role given to the GitHub identity.

targetScope = 'resourceGroup'

@description('Azure region. Must be allowed by the subscription policy.')
param location string = resourceGroup().location

@description('Base name used to build resource names.')
@minLength(3)
@maxLength(20)
param appName string = 'insureai'

@description('App Service plan SKU. F1 is free; use B1 for always-on and higher quotas.')
@allowed([
  'F1'
  'B1'
])
param skuName string = 'F1'

@description('Environment label.')
param environmentName string = 'dev'

var suffix = uniqueString(resourceGroup().id)
var planName = 'asp-${appName}-${environmentName}'
var webAppName = 'app-${appName}-${environmentName}-${suffix}'

resource plan 'Microsoft.Web/serverfarms@2024-04-01' = {
  name: planName
  location: location
  kind: 'linux'
  sku: {
	name: skuName
  }
  properties: {
	reserved: true // required for Linux plans
  }
}

resource webApp 'Microsoft.Web/sites@2024-04-01' = {
  name: webAppName
  location: location
  kind: 'app,linux'
  tags: {
	'azd-service-name': 'api'
  }
  properties: {
	serverFarmId: plan.id
	httpsOnly: true
	siteConfig: {
	  linuxFxVersion: 'DOTNETCORE|10.0'
	  minTlsVersion: '1.2'
	  ftpsState: 'Disabled'
	  alwaysOn: skuName == 'F1' ? false : true // always-on is not available on F1
	  http20Enabled: true
	  appSettings: [
		{
		  name: 'ASPNETCORE_ENVIRONMENT'
		  value: 'Production'
		}
	  ]
	}
  }
}

output webAppName string = webApp.name
output webAppUrl string = 'https://${webApp.properties.defaultHostName}'
