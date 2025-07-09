Feature: Language Management on Profile
  To ensure a user can add, edit, delete and validate language entries

  Background:
    Given I log into Localhost portal
    And I navigate to the Profile's Language section

  @Languages
  Scenario Outline: Add a language with valid input
    When I add the Language "<Language>" with level "<Level>"
    Then I verify language "<Language>" is "<ExpectedResult>" in the list

    Examples:
      | Language | Level          | ExpectedResult |
      | English  | Conversational | present        |
      | Hindi    | Fluent         | present        |
      | German   | Basic          | present        |
      | French   | Fluent         | present        |

  @Languages
  Scenario Outline: Add a language with invalid input
    When I add the Language "<Language>" with level "<Level>"
    Then I verify language "<Language>" is "<ExpectedResult>" in the list

    Examples:
      | Language | Level          | ExpectedResult |
      | 1234     | Basic          | not_present    |
      | !@#$%    | Fluent         | not_present    |
      |          | Basic          | not_present    |
      | Java     |                | not_present    |
      | Hcnerf   | Conversational | not_present    |
      | EnGlIsH  | Fluent         | not_present    |


  @Languages
  Scenario Outline: Add option not available when the maximum limit is reached
    Given I have added the following languages:
      | Language | Level          |
      | English  | Conversational |
      | Hindi    | Fluent         |
      | German   | Basic          |
      | French   | Fluent         |
    When I add the Language "<Language>" with level "<Level>"
    Then I verify language "<Language>" is "<ExpectedResult>" in the list

    Examples:
      | Language | Level | ExpectedResult |
      | Spanish  | Basic | not_added      |

  @Languages
  Scenario Outline: Edit an existing language
    When I edit the Language "<OldLanguage>" to "<NewLanguage>" with level "<Level>"
    Then I verify language "<NewLanguage>" is "<ExpectedResult>" in the list

    Examples:
      | OldLanguage | NewLanguage | Level          | ExpectedResult |
      | Hindi       | Urdu        | Conversational | present        |
      | German      | @@@@        | Fluent         | present        |

  @Languages
  Scenario Outline: Delete an existing language from profile
    When I delete the Language "<Language>"
    Then I verify language "<Language>" is "<ExpectedResult>" in the list

    Examples:
      | Language | ExpectedResult |
      | Urdu     | not_present    |
      | @@@@     | not_present    |
      | French   | not_present    |

  @Languages
  Scenario Outline: Add a language with destructive input
    When I add the Language "<Language>" with level "<Level>"
    Then I verify language "<Language>" is "<ExpectedResult>" in the list

    Examples:
      | Language                          | Level         | ExpectedResult             |
      | <script>alert("x")</script>       | Basic         | Invalid characters found   |
      | DROP TABLE Languages;--           | Fluent        | Invalid characters found   |
      | aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa | Conversational | Language name too long |

  @Languages
  Scenario Outline: Add a duplicate language (Negative with valid input)
    Given I have added the Language "<Language>" with level "<Level>"
    When I try to add the Language "<Language>" again with level "<Level>"
    Then I verify error message "<ExpectedResult>" is shown

    Examples:
      | Language | Level          | ExpectedResult          |
      | English  | Conversational | Language already exists |
