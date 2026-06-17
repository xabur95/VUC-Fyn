Feature: Admin School Overview

  As an admin
  I want to see all registered schools
  So that I can manage the school directory

  Background:
    Given the system has 2 registered schools

  Scenario: Admin sees all registered schools
    When the admin requests the school overview
    Then the overview should contain 2 schools
