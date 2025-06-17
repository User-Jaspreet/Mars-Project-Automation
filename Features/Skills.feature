Feature: Skill Management on Profile
  To ensure a user can add, edit, delete and validate skill entries

  Background:
    Given I log into Localhost portal
    And I navigate to the Profile's Skills section

  @Skills
  Scenario Outline: Add a skill with valid or invalid input
    When I add the Skill "<Skill>" with level "<Level>"
    Then I verify skill "<Skill>" is "<ExpectedResult>" in the list

    Examples:
      | Skill                                                                | Level        | ExpectedResult  |
      | Testing                                                              | Expert       | present         |
      | Testing                                                              | Expert       | not_duplicated  |
      | 0987                                                                 | Expert       | present     |
      | &^#@)}:                                                              | Intermediate | present         |
      | aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa | Beginner     | present         |

  @Skills
  Scenario Outline: Edit an existing skill
    When I edit the Skill "<OldSkill>" to "<NewSkill>" with level "<Level>"
    Then I verify skill "<NewSkill>" is "<ExpectedResult>" in the list

    Examples: 
      | OldSkill | NewSkill   | Level  | ExpectedResult  |
      | Testing  | Automation | Expert | present         |
      | 0987     | Coding     | Expert | present         |

  @Skills
  Scenario Outline: Delete a skill from profile
    When I delete the Skill "<Skill>"
    Then I verify skill "<Skill>" is "<ExpectedResult>" in the list

    Examples:
      | Skill      | ExpectedResult  |
      | Automation | not_present     |
      | Coding     | not_present     |
