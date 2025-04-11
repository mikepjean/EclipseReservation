# Security Policy

## Strategies for Securing the APIs

### Authentication
- **JWT-Based Authentication**: All API endpoints are secured using JSON Web Tokens (JWT). Tokens are issued upon successful user login and must be included in the `Authorization` header of subsequent requests in the format: `Bearer <token>`.
- **Token Validation**: Tokens are validated for issuer, audience, expiration, and signature using the `JwtBearer` middleware.

### Authorization
- **Role-Based Access Control (RBAC)**: Specific endpoints are restricted based on user roles. The `[Authorize]` attribute is used to enforce access control.
- **Token Claims**: User roles and permissions are embedded in the JWT claims to determine access levels.

### Data Validation
- **Input Validation**: All user inputs are validated at the controller level to ensure they meet expected formats and constraints.
- **Model Binding**: Strongly-typed models are used to enforce data integrity.
- **Sanitization**: Inputs are sanitized to prevent injection attacks.

---

## Security Headers and Configurations
The following HTTP security headers are configured to safeguard endpoints:
- **`Content-Security-Policy`**: Restricts the sources of content to prevent XSS attacks.
- **`X-Content-Type-Options: nosniff`**: Prevents MIME type sniffing.
- **`Strict-Transport-Security`**: Enforces HTTPS connections.
- **`X-Frame-Options: DENY`**: Prevents clickjacking by disallowing the site to be embedded in iframes.
- **`Referrer-Policy: no-referrer`**: Limits the amount of referrer information sent with requests.

---

## Protocols to Protect Against Common Vulnerabilities

### SQL Injection
- **Parameterized Queries**: All database interactions use parameterized queries to prevent SQL injection attacks.
- **ORM Usage**: An Object-Relational Mapper (ORM) is used to abstract database interactions and reduce the risk of injection.

### Cross-Site Scripting (XSS)
- **Input Sanitization**: User inputs are sanitized to remove malicious scripts.
- **Output Encoding**: Data rendered in views or responses is encoded to prevent script execution.

### Cross-Site Request Forgery (CSRF)
- **CSRF Tokens**: State-changing requests are protected using anti-CSRF tokens.
- **SameSite Cookies**: Cookies are configured with the `SameSite` attribute to prevent cross-origin requests.

---

## Monitoring and Logging

### Logging
- **Request Logging**: All API requests are logged, including IP addresses, user agents, and request paths.
- **Error Logging**: Errors and exceptions are logged with stack traces for debugging and auditing purposes.
- **Sensitive Data Masking**: Sensitive information (e.g., passwords, tokens) is masked in logs.

### Monitoring
- **Suspicious Activity Detection**: Logs are periodically reviewed for unusual patterns, such as repeated failed login attempts or high-frequency requests.
- **Rate Limiting**: Rate limiting is implemented to prevent abuse and denial-of-service attacks.
- **Alerts**: Alerts are configured to notify administrators of potential security breaches.

---

## Reporting a Vulnerability
If you discover a security vulnerability, please follow these steps:
1. **Do not disclose it publicly.**
2. Contact us via email at [mike.p.jean@outlook.com](mailto:mike.p.jean@outlook.com).
3. Provide a detailed description of the vulnerability, including steps to reproduce it.

We will respond to your report within 48 hours and work with you to resolve the issue.

---

## Contact
For any security-related concerns, please contact us at [mike.p.jean@outlook.com](mailto:mike.p.jean@outlook.com).
