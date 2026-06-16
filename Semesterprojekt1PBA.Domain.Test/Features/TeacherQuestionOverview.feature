Feature: Teacher Question Overview

  As a teacher
  I want to see an overview of questions, create a new question, and see it in the updated overview
  So that I can manage my questions effectively

  Scenario: Teacher creates a new question and sees it in the overview
    Given a teacher "Tina Teacher" with email "tina@vucfyn.dk" exists
    And the following questions exist for the teacher
      | Title              | Text                        | Points |
      | What is DDD?       | Explain Domain-Driven Design | 10     |
      | What is CQRS?      | Explain CQRS pattern         | 5      |
    When the teacher views the question overview
    Then the overview should contain 2 questions
    When the teacher creates a new question with title "What is TDD?" and text "Explain Test-Driven Development" and points 8
    And the teacher views the question overview
    Then the overview should contain 3 questions
    And the overview should contain a question with title "What is TDD?"
