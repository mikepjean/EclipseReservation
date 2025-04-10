## Objective

Develop "EclipseLink," a backend service using C# and .Net for astronomy enthusiasts to discover, RSVP, and interact with solar eclipse events. Your mission is to create a robust backend system that efficiently manages event data, user registrations, notifications, and ensures endpoint security.

## Brief

EclipseLink is poised to be the premier backend service for managing data related to solar eclipse events. Your task is to construct the backend infrastructure allowing users to find events, RSVP, and receive notifications. Additionally, you will outline a security strategy to protect the service's endpoints.

## Tasks

1. **User Authentication Module**: Implement a module that supports user registration, login, and profile management. User profiles should include preferences for receiving notifications about eclipse events.

2. **Event Model Structure**:
   - `Event`: Design an `Event` model with attributes like `name`, `description`, `event_date`, `visibility_locations`, and `rsvp_count`.
   - `UserEvent`: Establish a `UserEvent` model to link users with their RSVPs to events, featuring `user_id`, `event_id`, and `status`.

3. **Event Discovery and RSVP API**: Build APIs that enable users to:
   - View upcoming solar eclipse events, filterable by date and location.
   - RSVP to events, which updates the `UserEvent` relationship and the event's `rsvp_count`.

4. **Eclipse Event Notifications Service**: Create a service that sends out notifications to users about the eclipse events they've RSVP'd to, with reminders at strategic intervals.

5. **Optional - Community Interaction Features**: (Optional) Develop APIs for users to interact with event pages through comments.

6. **Security Task - SECURITY.md**: Craft a SECURITY.md file detailing:
   - Your strategies for securing the APIs, including methods for authentication, authorization, and data validation.
   - Specific security headers or configurations to safeguard your endpoints.
   - Protocols to protect the application against common web vulnerabilities such as SQL injection, XSS, and CSRF.
   - Approaches to monitor and log endpoint access for detecting and addressing suspicious activities.

## Evaluation Criteria

- **API Design and Implementation**: APIs should be RESTful, well-organized, and efficiently handle user interactions.
- **Model Design**: Event and UserEvent models must be thoroughly designed to capture essential data and relationships.
- **Security Documentation**: SECURITY.md should clearly articulate the security measures and the reasoning behind them to protect the endpoints.
- **Code Quality**: Code should be clean, well-commented, and adhere to best practices in C# and .Net.

## CodeSubmit

Please organize, design, test, and document your code as if it were going into production - then push your changes to the master branch. After you have pushed your code, you may submit the assignment on the assignment page.

All the best and happy coding,

The Elevate Digital Team