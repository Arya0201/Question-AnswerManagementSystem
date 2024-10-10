# QA Management System

## Objectives

- Implement a system for creating, managing, and submitting question papers.
- Provide role-based access control for administrators, teachers, and students.
- Ensure timestamped recording of answers for tracking and auditing purposes.

## Outcomes

- Users can efficiently manage question papers through dedicated panels.
- Seamless submission and timestamping of answers ensure accurate tracking and audit trails.
- Centralized database storage allows for easy retrieval and analysis of submitted question papers.

## Approach

1. *System Setup:* 
   - Establish the project structure and database schema.
2. *User Management:*
   - Implement role-based access control for administrators, teachers, and students.
3. *Question Paper Creation:*
   - Develop functionality for teachers to create new question papers and submit them for approval.
4. *Submission Handling:*
   - Enable students to submit answers to question papers.
   - Implement timestamping to record submission times.
5. *Database Interaction:*
   - Utilize Entity Framework for seamless interaction with the Microsoft SQL Server database.
6. *User Interface:*
   - Design intuitive user interfaces for administrators, teachers, and students to interact with the system.

## Setup Instructions

Follow these steps to set up the project:

1. Create a database named `QAManagementSystem` in Microsoft SQL Server.
2. Run the provided `QAManagementSystemDatabase.sql` script to create tables and populate initial data.
3. Update the connection string in the `web.config` file to connect the application to the database.

## Login Credentials

Use the following credentials to access different roles within the system:

- *Admin:*
  - Email: arya@gmail.com
  - Password: arya123

- *Teacher:*
  - Email: teacher@gmail.com
  - Password: teacher123

- *Student:*
  - Email: student@gmail.com
  - Password: student123
  - 
## Overview

The QA Management System facilitates the creation, management, and submission of question papers for educational purposes. It offers distinct functionalities for administrators, teachers, and students, streamlining the process of handling question papers throughout their lifecycle.

## Features

- *Admin Panel:*
  - Create and manage users (teachers and students).
  - Manage question papers.

- *Teacher Panel:*
  - Create new question papers.
  - Send question papers to admin for approval.

- *Student Panel:*
  - Submit answers to question papers.
  - View submitted question papers.

- *Timestamping:*
  - Answers are timestamped upon submission for record-keeping and tracking purposes.

## Technologies Used

- *C#:* Backend development language.
- *ASP.NET MVC:* Framework for web application development.
- *Entity Framework:* ORM tool for database interaction.
- *HTML/CSS:* Frontend design.
- *JavaScript:* Enhancing frontend interactions.
- *Microsoft SQL Server:* Database management system.

## Database Details (QAManagementSystem)

The database schema includes the following tables:

- *Users:* Stores user information for admins, teachers, and students.
- *QuestionPaper:* Contains details about the question papers.
- *Questions:* Stores questions for each question paper.
- *Answer:* Records answers submitted by students.
- *SubmittedQuestionPapers:* Logs submitted question papers.
