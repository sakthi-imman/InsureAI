# Business Requirements Document (BRD)

**Project:** InsureAI: Vehicle Insurance Platform (ASP.NET Core Web API)
**Version:** 0.1 (Draft)
**Platform:** .NET 10, ASP.NET Core Web API
**Status:** Draft for review

---

## 1. Document Control

| Item | Detail |
|---|---|
| Document owner | Product Owner (TBD) |
| Technical owner | Engineering Lead (TBD) |
| Stakeholders | Business, Underwriting, Claims, Compliance, IT/Security |
| Related documents | Technical design, API specification (OpenAPI), test plan (to be created) |

### Revision History

| Version | Date | Author | Description |
|---|---|---|---|
| 0.1 | TBD | TBD | Initial draft based on the current codebase and the proposed roadmap |

---

## 2. Executive Summary

InsureAI is a backend platform that supports the vehicle insurance lifecycle: vehicle registration lookup, quoting, policy issuance, claims, and AI-assisted risk assessment. The platform exposes a secure, versioned REST API for web, mobile, and partner (agent/broker) channels.

The current release (v0.1) is a proof of concept. It provides a vehicle lookup by registration number from in-memory data. This document defines the business requirements to evolve it into an enterprise-grade, end-to-end insurance solution.

---

## 3. Business Objectives

| ID | Objective | Success Measure |
|---|---|---|
| BO-1 | Digitize vehicle insurance operations end to end | Quote-to-policy completed fully online |
| BO-2 | Reduce quote and policy issuance time | Quote response under 3 seconds; issuance under 5 minutes |
| BO-3 | Improve underwriting accuracy using data and AI | Measurable improvement in loss ratio |
| BO-4 | Speed up claims handling and reduce fraud | Reduced average claim cycle time; fraud flags raised before payout |
| BO-5 | Provide a secure, compliant and auditable platform | Zero critical audit findings; privacy regulations met |
| BO-6 | Enable multi-channel access (web, mobile, partners) | Public API consumed by at least 3 channels |

---

## 4. Scope

### 4.1 In Scope
- Vehicle management and lookup
- Customer management
- Quote and premium calculation
- Policy lifecycle (issue, endorse, renew, cancel)
- Claims intake and workflow
- Payments integration (via a payment gateway)
- Document handling (policy documents, claim evidence)
- AI-assisted risk scoring and fraud indicators
- Authentication, authorization, audit trail
- Notifications (email/SMS)
- Reporting and operational dashboards

### 4.2 Out of Scope (initial releases)
- Non-motor insurance lines (health, life, property)
- Reinsurance and general ledger accounting
- Full front-end applications (the API is the deliverable)
- Physical vehicle inspection hardware integrations

### 4.3 Assumptions
- A relational database (SQL Server or PostgreSQL) will be provisioned.
- Hosting will be on Azure (App Service or Container Apps).
- Identity will be provided by Microsoft Entra ID or an OIDC-compatible provider.
- Third-party services (payment gateway, vehicle registry, notification provider) will be available via APIs.

### 4.4 Constraints
- Must comply with applicable data protection and insurance regulations.
- Must be built and maintained on .NET 10.
- Personally identifiable information (PII) must be encrypted in transit and at rest.

---

## 5. Stakeholders and User Roles

| Role | Description | Key Needs |
|---|---|---|
| Customer | Vehicle owner buying or managing insurance | Self-service quotes, policies, claims |
| Agent/Broker | Sells on behalf of the insurer | Create quotes and policies for customers |
| Underwriter | Assesses and approves risk | Risk data, scoring, referral queue |
| Claims Handler | Processes claims | Claim workflow, evidence, decisions |
| Administrator | Operates the platform | User management, configuration, audit |
| Compliance/Auditor | Oversight | Audit logs, reports, data access control |
| External System | Payment, registry, notification services | Reliable, secure integration |

---

## 6. Current State (v0.1)

| Area | Status |
|---|---|
| Vehicle model | Implemented: `RegistrationNumber`, `Make`, `Model`, `Year`, `InsuredDeclaredValue` |
| Vehicle service | Implemented with in-memory sample data (2 vehicles); case-insensitive lookup |
| API endpoint | `GET /api/vehicles/{registrationNumber}` returns 200 (found) or 404 (not found) |
| Dependency injection | `IVehicleService` registered as singleton |
| OpenAPI | Enabled in Development |
| Persistence, security, validation, logging, tests | Not implemented |

---

## 7. Functional Requirements

Priority: **M** = Must have, **S** = Should have, **C** = Could have.

### 7.1 Vehicle Management

| ID | Requirement | Priority | Status |
|---|---|---|---|
| FR-VEH-01 | Retrieve a vehicle by registration number (case-insensitive) | M | Done (in-memory) |
| FR-VEH-02 | Create, update and deactivate vehicles | M | Planned |
| FR-VEH-03 | Search and filter vehicles with pagination and sorting | S | Planned |
| FR-VEH-04 | Validate registration number format, year range and declared value | M | Planned |
| FR-VEH-05 | Integrate with an external vehicle registry to verify details | C | Planned |

### 7.2 Customer Management

| ID | Requirement | Priority |
|---|---|---|
| FR-CUS-01 | Register and maintain customer profiles (personal, contact, address) | M |
| FR-CUS-02 | Link customers to vehicles and policies | M |
| FR-CUS-03 | Capture driver details and history (licence, claims history) | S |
| FR-CUS-04 | Manage consent and communication preferences | M |

### 7.3 Quotes and Premium Calculation

| ID | Requirement | Priority |
|---|---|---|
| FR-QUO-01 | Generate a quote from vehicle, driver and coverage inputs | M |
| FR-QUO-02 | Calculate premium using configurable rating rules (vehicle age, value, usage, driver profile) | M |
| FR-QUO-03 | Support coverage options (third party, comprehensive, add-ons) | M |
| FR-QUO-04 | Quotes expire after a configurable validity period | S |
| FR-QUO-05 | Refer high-risk quotes to an underwriter | S |

### 7.4 Policy Management

| ID | Requirement | Priority |
|---|---|---|
| FR-POL-01 | Convert an accepted quote into a policy after payment | M |
| FR-POL-02 | Generate a policy number and policy document | M |
| FR-POL-03 | Support endorsements (change of vehicle, coverage, declared value) | S |
| FR-POL-04 | Support renewal, with reminders before expiry | M |
| FR-POL-05 | Support cancellation with a pro-rata refund calculation | S |
| FR-POL-06 | Maintain the full policy status history (Quoted, Bound, Active, Lapsed, Cancelled, Expired) | M |

### 7.5 Claims Management

| ID | Requirement | Priority |
|---|---|---|
| FR-CLM-01 | Submit a claim against an active policy | M |
| FR-CLM-02 | Upload supporting evidence (photos, reports) | M |
| FR-CLM-03 | Claim workflow: Submitted, Under Review, Approved/Rejected, Settled | M |
| FR-CLM-04 | Assign claims to handlers and track SLAs | S |
| FR-CLM-05 | Validate claims against policy coverage and the declared value | M |
| FR-CLM-06 | Record settlement and payment details | M |

### 7.6 AI-Assisted Capabilities

| ID | Requirement | Priority |
|---|---|---|
| FR-AI-01 | Risk scoring to support underwriting decisions | S |
| FR-AI-02 | Fraud indicators on claims, with explainable reasons | S |
| FR-AI-03 | Damage assessment from uploaded images | C |
| FR-AI-04 | Customer-facing claims assistant | C |
| FR-AI-05 | All AI outputs are advisory; a human makes the final decision on rejection or high-value claims | M |

### 7.7 Payments

| ID | Requirement | Priority |
|---|---|---|
| FR-PAY-01 | Collect premium via an external payment gateway | M |
| FR-PAY-02 | Support instalment plans | C |
| FR-PAY-03 | Record payment status and reconcile with policies | M |
| FR-PAY-04 | Process refunds and claim payouts | S |

### 7.8 Security and Access

| ID | Requirement | Priority |
|---|---|---|
| FR-SEC-01 | Authenticate all API users (OIDC/JWT) | M |
| FR-SEC-02 | Role-based authorization (Customer, Agent, Underwriter, Claims Handler, Admin) | M |
| FR-SEC-03 | Audit log of create, update, delete and sensitive read actions | M |
| FR-SEC-04 | Rate limiting and CORS policy | S |

### 7.9 Notifications and Reporting

| ID | Requirement | Priority |
|---|---|---|
| FR-NOT-01 | Send email/SMS for quote, policy, renewal, claim status and payment events | S |
| FR-REP-01 | Operational reports: policies issued, premiums, claims, loss ratio | S |
| FR-REP-02 | Export reports (CSV/PDF) | C |

---

## 8. Non-Functional Requirements

| ID | Category | Requirement |
|---|---|---|
| NFR-01 | Performance | 95th percentile API response under 500 ms for lookups; quote under 3 s |
| NFR-02 | Availability | 99.9% monthly uptime for production |
| NFR-03 | Scalability | Horizontal scaling; stateless API instances |
| NFR-04 | Security | OWASP Top 10 mitigations; TLS 1.2+; secrets in a vault; PII encrypted at rest |
| NFR-05 | Privacy and compliance | Applicable data protection laws (e.g., GDPR); data retention and deletion policy |
| NFR-06 | Reliability | Retries, timeouts and circuit breakers on external calls; health checks |
| NFR-07 | Observability | Structured logging, distributed tracing, metrics and alerting |
| NFR-08 | Maintainability | Layered architecture; at least 80% unit test coverage on business logic |
| NFR-09 | Interoperability | Versioned REST API with OpenAPI documentation; JSON payloads |
| NFR-10 | Auditability | Immutable audit trail for policy and claim changes |
| NFR-11 | Disaster recovery | Defined RPO (15 minutes) and RTO (4 hours); automated backups |
| NFR-12 | Deployment | Containerized; CI/CD pipeline; infrastructure as code; separate dev/test/prod environments |

---

## 9. Business Rules

| ID | Rule |
|---|---|
| BR-01 | Registration numbers are unique per active policy and compared case-insensitively. |
| BR-02 | The insured declared value must be greater than zero and is the maximum basis for a total-loss settlement. |
| BR-03 | A claim can be raised only against a policy that is active on the date of loss. |
| BR-04 | A quote cannot be bound after its expiry date. |
| BR-05 | Policies above a configurable declared value threshold require underwriter approval. |
| BR-06 | Claim rejections and settlements above a threshold require human approval, regardless of AI recommendations. |
| BR-07 | Cancelled policies are refunded pro rata, less any applicable fees. |

---

## 10. High-Level Architecture and Data Overview

### 10.1 Target Architecture
- **API layer:** ASP.NET Core controllers/minimal APIs, versioning, validation, `ProblemDetails` error handling
- **Application layer:** services, DTOs, validators, workflows
- **Domain layer:** entities and business rules
- **Infrastructure layer:** EF Core persistence, external integrations (payments, notifications, AI, storage)

### 10.2 Core Entities

| Entity | Key Attributes |
|---|---|
| Vehicle | RegistrationNumber, Make, Model, Year, InsuredDeclaredValue |
| Customer | Name, contact details, address, consent |
| Quote | Vehicle, customer, coverage, premium, expiry |
| Policy | PolicyNumber, status, dates, coverage, premium |
| Claim | ClaimNumber, policy, loss date, description, status, amount |
| Payment | Amount, method, status, reference |
| Document | Type, storage location, linked entity |
| AuditLog | Actor, action, entity, timestamp |

### 10.3 Integrations

| System | Purpose |
|---|---|
| Identity provider (Entra ID/OIDC) | Authentication and roles |
| Payment gateway | Premium collection, refunds |
| Notification provider | Email/SMS |
| Blob storage | Documents and images |
| AI services (Azure OpenAI/Foundry) | Risk scoring, fraud, image analysis |
| Vehicle registry (optional) | Verify vehicle details |

---

## 11. Phased Delivery Roadmap

| Phase | Focus | Key Deliverables |
|---|---|---|
| 0 | Stabilize | DI registration, `IVehicleService`, fixed `.http` file, unit test project |
| 1 | Foundation | Layered structure, EF Core persistence, DTOs, validation, global error handling, API versioning, configuration and secrets |
| 2 | Insurance domain | Customers, quotes, policies, claims, premium rules, documents, AI risk scoring |
| 3 | Security and compliance | Authentication, RBAC, audit logging, rate limiting, PII protection |
| 4 | Reliability and operations | Logging and telemetry, health checks, resilience, caching, background jobs, messaging |
| 5 | Delivery | Docker, CI/CD, infrastructure as code, automated integration tests, security scanning |

---

## 12. Acceptance Criteria (Summary)

1. All Must-have functional requirements are implemented and pass UAT.
2. API contracts are documented via OpenAPI and are backward compatible within a version.
3. Authentication and role-based authorization are enforced on all non-public endpoints.
4. Automated tests run in CI, and the build fails on test failure.
5. Performance and availability targets (NFR-01, NFR-02) are verified in a pre-production environment.
6. Security review and vulnerability scan show no unresolved critical or high findings.
7. Audit logs capture all required events.

---

## 13. Risks and Mitigations

| ID | Risk | Impact | Mitigation |
|---|---|---|---|
| R-01 | Regulatory requirements change or are misunderstood | High | Early compliance review; configurable rules |
| R-02 | AI outputs are inaccurate or biased | High | Human-in-the-loop, explainability, monitoring, model evaluation |
| R-03 | Third-party integration delays or outages | Medium | Abstractions, resilience policies, fallback paths |
| R-04 | PII breach | High | Encryption, least-privilege access, audit, penetration testing |
| R-05 | Scope creep | Medium | Phased delivery, change control |
| R-06 | Inaccurate premium rules affect profitability | High | Actuarial review, versioned rules, test scenarios |

---

## 14. Dependencies

- Provisioned Azure subscription and environments
- Identity provider tenant and app registrations
- Payment gateway and notification provider accounts
- Approved rating and underwriting rules from business
- Legal and compliance sign-off on data handling

---

## 15. Open Questions

1. Which jurisdictions and regulators apply?
2. Which database engine is preferred (SQL Server or PostgreSQL)?
3. Which payment gateway will be used?
4. Are agent and broker channels required in the first production release?
5. What are the underwriting thresholds for referral and approval?
6. Is integration with a national or external vehicle registry available?
7. What are the data retention periods for policies, claims and documents?

---

## 16. Approval

| Name | Role | Signature | Date |
|---|---|---|---|
| | Business Sponsor | | |
| | Product Owner | | |
| | Engineering Lead | | |
| | Compliance/Security | | |
