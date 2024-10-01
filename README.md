# ECommerce API

## Overview

The ECommerce API is a robust .NET web API designed to facilitate an online shopping platform. Built using modern architectural patterns and best practices, this API supports various features to enhance user experience and administrative control.

## Features

### Server-Side

- **Architecture**: Implemented using Onion Architecture, promoting separation of concerns and maintainability.
- **Database**: Utilizes PostgreSQL for efficient data storage and retrieval.
- **CQRS and Mediator**: Applied the Command Query Responsibility Segregation (CQRS) pattern with Mediator for improved handling of requests and responses.
- **Repository Pattern**: Ensured a clean separation between data access and business logic.
- **Authentication**:
  - **Token Authentication**: Integrated access and refresh tokens for secure user authentication.
  - **Social Authentication**: Enabled Google and Facebook authentication for user convenience.
- **File Storage**: Used Azure Blob Storage for storing product images and other files.
- **Logging**: Implemented Serilog for comprehensive logging and error tracking.
- **Real-Time Communication**: Leveraged SignalR for real-time notifications and updates.
- **Email Notifications**: Configured SMTP to send email notifications for order confirmations and other alerts.
- **Role-Based Access Control**: Added role permission filters to secure endpoints based on user roles.
- **QR Code Integration**: Implemented QR codes for products; users can scan codes to view product details instantly.

### Client-Side

- **Framework**: Built with Angular for a dynamic and responsive user interface.
- **UI Components**: Utilized Bootstrap for layout, along with Angular Material for modern design components.
- **Notifications**: Integrated AlertifyJS and Toastr for user feedback and alerts.
- **Loading Indicators**: Employed ngx-spinner to indicate loading states during data operations.
- **File Uploads**: Implemented `ngx-file-drop` for an intuitive drag-and-drop file upload experience.

### Admin Features

- **Admin Dashboard**: Admins can view and manage all orders placed through the platform.
- **User Management**: Admins have the ability to grant specific permissions to users for various endpoints, ensuring appropriate access control.

## Functionalities

- **CRUD Operations**: Perform create, read, update, and delete operations on products.
- **Filtering**: Users can filter products based on various criteria for easier navigation.
- **Authentication**: Secure user authentication using token-based and social login methods.
- **Ordering**: Users can place orders, and receive SMS notifications upon order confirmation.
