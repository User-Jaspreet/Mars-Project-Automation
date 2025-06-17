Feature: Language Management on Profile
  To ensure a user can add, edit, delete and validate language entries

  Background:
    Given I log into Localhost portal
    And I navigate to the Profile's Language section

  @Languages
  Scenario Outline: Add a language with valid or invalid input
    When I add the Language "<Language>" with level "<Level>"
    Then I verify language "<Language>" is "<ExpectedResult>" in the list

    Examples:
      | Language                                                               | Level           | ExpectedResult  |
      | English                                                                | Conversational  | present         |
      | English                                                                | Conversational  | not_duplicated  |
      | Hindi                                                                  | Fluent          | present         |
      | German                                                                 | Basic           | present         |
      | French                                                                 | Fluent          | present         |
      | 1234                                                                   | Basic           | present         |
      | !@#$%                                                                  | Fluent          | present         |
      | aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa | Conversational  | present         |

      
    @Languages
    Scenario: Prevent adding more than 4 languages
    Examples: 
    | Language |       Level          |  ExpectedResult |
    | English  |   Conversational     |   present       |
    | Hindi    |   Fluent             |   present       |
    | German   |   Basic              |   present       |
    | French   |   Fluent             |   present       |
    | Spanish  |   Basic              |   not_present   |



  @Languages
  Scenario Outline: Edit an existing language
    When I edit the Language "<OldLanguage>" to "<NewLanguage>" with level "<Level>"
    Then I verify language "<NewLanguage>" is "<ExpectedResult>" in the list

    Examples:
      | OldLanguage | NewLanguage | Level          | ExpectedResult  |
      | Hindi       | Urdu        | Conversational | present         |
      | German      | @@@@        | Fluent         | present         |

  @Languages
  Scenario Outline: Delete a language from profile
    When I delete the Language "<Language>"
    Then I verify language "<Language>" is "<ExpectedResult>" in the list

    Examples:
      | Language | ExpectedResult  |
      | Urdu     | not_present     |
      | @@@@     | not_present     |
      | French   | not_present     |

 
  
