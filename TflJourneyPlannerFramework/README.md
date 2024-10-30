# TFL Journey Planner widget UI Automation

## Overview
This is a UI automation framework developed to perform key functionalities tests of the TFL Journey Planner widget. The goal is to validate the journey planner's capability in handling different user inputs and preferences; hence, finding a route will be effective and handle edge cases, such as invalid inputs.
Selenium WebDriver used for UI interactions, SpecFlow used for behavior-driven development, and C# ensure that the test suite will be readable, maintainable, and scalable for the project. Tests are written in Gherkin syntax so that the scenarios can be understood with ease by both technical and nontechnical stakeholders.

## Project Structure
SpecFlow-Gherkin: This automated test framework is developed by using the SpecFlow plug-in inside Visual Studio to enable BDD-style tests. It uses the Gherkin syntax for defining feature files in a business-readable style.
Selenium WebDriver and ChromeDriver: Selenium is used to simulate user interactions against the TFL Journey Planner widget, while ChromeDriver allows these tests to run on the Chrome browser.
Test Scenarios: The framework covers five of the major test scenarios, including simple journey planning, preference update, detail journey information, validity of error handling on invalid and empty input.
## Setup Instructions
- Clone the repository.
- Open the project in Visual Studio.
- Ensure that Selenium, SpecFlow, and ChromeDriver are installed
- Run the tests using Visual Studio Test Explorer.

## Development Decisions
- **SpecFlow** -It is used to structure the project in BDD format to making the tests clear and accessible.
- **Selenium WebDriver**- Selenium  provides cross-browser support and can integrates well with .NET.It is open source and can be compatible with multiple progarmming languages.
- **Hooks**- I have used hooks concepts before and after scenarios to create each scearios as indipendent.This can increase the stability of the automation suite and  easy maintenance.
- **TestData**-Paramerarized the test data by using the example tables.This approach will increase code reusability and reduce maintenance time.
-**Visual Studio**- Used visualstudio for implementation of framework as an IDE.It provides built in support for popular frameworks like NUnit,MSTest.

## Additional Features can be implemented in this Framework

- Page Object Model design 
- HTML or Extended report
