# Book Management Web App - QA Solution

## Overview

This repository contains the complete Quality Assurance solution. It includes:
- Test plans for both API and UI
- Automated API integration tests and UI tests
- Manual UI test cases
- A comprehensive Postman collection for API validation with environment variables and tests with validations

## Repository Structure
/API_Tests # Automated API integration tests (NUnit, RestSharp, Newtonsoft)
/UI_Tests # Automated UI tests (NUnit, Selenium)
/Core # Page Objects, API clients, utilities, config

/Core/Docs
UI_Testcases  # Manual UI test cases
API_Testplan.docx # API test plan
UI_Testplan.docx # UI test plan
Bugs.docx # Reported bugs


/Core/Postman
postman_tests.json # Postman API test collection
postman_environment # Postman Environment

**Coverage includes:**
- Navigation and screen access
- CRUD operations for book requests
- UI element presence and functionality
- Data validation and updates
- Redirection, URL checks, and cancel/confirm flows

Each test case includes:
- ID, Title, Description, Steps, Preconditions, Expected Behavior

## Defect Tracking

Defects are tracked in .docx file. They include Title, Description, Steps to reproduce, Expected and Atual behavior.
