Feature: Test scenarios for Languages and Skills

@Languages
Scenario: A. Add a Language in Profile with valid name and level
  Given I log into Localhost portal
  When I navigate to the Profile's Language section
  And I add the Language 'English' with level 'Conversational'
  Then The Language 'English' should appear in the list

Scenario: B. Add a duplicate Language (valid input, negative logic)
  Given I log into Localhost portal
  When I navigate to the Profile's Language section
  And I try to add the Language 'English' again with level 'Conversational'
  Then The Language 'English' should not be duplicated in the list

Scenario: C. Add a Language in Profile with valid name and level
  Given I log into Localhost portal
  When I navigate to the Profile's Language section
  And I add the Language 'Hindi' with level 'Fluent' 
  Then The Language 'Hindi' should appear in the list

Scenario: D. Edit a Language in Profile
  Given I log into Localhost portal
  When I navigate to the Profile's Language section
  And I edit the Language 'Hindi' 'Urdu' with level 'Conversational' 
  Then The Language 'Urdu' should appear in the list

Scenario: E. Add a Language in Profile with valid name and level
  Given I log into Localhost portal
  When I navigate to the Profile's Language section
  And I add the Language 'German' with level 'Basic' 
  Then The Language 'German' should appear in the list

Scenario: F. Edit a Language to use symbols (destructive test)
  Given I log into Localhost portal
  When I navigate to the Profile's Language section
  And I edit the Language 'German' '@@@@' with level 'Fluent'
  Then The Language '@@@@' should appear in the list

Scenario: G. Add a Language in Profile with valid name and level
  Given I log into Localhost portal
  When I navigate to the Profile's Language section
  And I add the Language 'French' with level 'Fluent' 
  Then The Language 'French' should appear in the list

Scenario: H. Add a Language in Profile with valid name and level
  Given I log into Localhost portal
  When I navigate to the Profile's Language section
  And I try to add the Language 'Spanish' with level 'Basic' 
  Then The Language 'Spanish' should not be added

Scenario: I. Delete a Language in Profile with valid name and level
  Given I log into Localhost portal
  When I navigate to the Profile's Language section
  And I delete the Language 'Urdu' with level 'Conversational'
  Then The Language 'Urdu' should not appear in the list

Scenario: J. Delete a Language in Profile with valid name and level
  Given I log into Localhost portal
  When I navigate to the Profile's Language section
  And I delete the Language '@@@@' with level 'Fluent'
  Then The Language '@@@@' should not appear in the list

Scenario: K. Delete a Language in Profile with valid name and level
  Given I log into Localhost portal
  When I navigate to the Profile's Language section
  And I delete the Language 'French' with level 'Fluent'
  Then The Language 'French' should not appear in the list

Scenario: L. Add a Language with empty fields
  Given I log into Localhost portal
  When I navigate to the Profile's Language section
  And I try to add the Language ' ' with level ' '
  Then The Language ' ' should not be added

Scenario: M. Add a Language with only numbers (invalid input)
  Given I log into Localhost portal
  When I navigate to the Profile's Language section
  And I try to add the Language '1234' with level 'Basic'
  Then The Language '1234' should be added

Scenario: N. Add a Language with special characters (destructive input)
  Given I log into Localhost portal
  When I navigate to the Profile's Language section
  And I try to add the Language '!@#$%' with level 'Fluent'
  Then The Language '!@#$%' should be added

Scenario: O. Add a Language with a very long string
  Given I log into Localhost portal
  When I navigate to the Profile's Language section
  And I try to add the Language 'aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa' with level 'Conversational'
  Then The Language 'aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa' should be added

@Skills
Scenario: A. Add a Skill with valid name and level
  Given I log into Localhost portal
  When I navigate to the Profile's Skills section
  And I add the Skill 'Testing' with level 'Expert'
  Then The Skill 'Testing' should appear in the list

Scenario: B. Add a duplicate Skill
  Given I log into Localhost portal
  When I navigate to the Profile's Skills section
  And I try to add the Skill 'Testing' again with level 'Expert'
  Then The Skill 'Testing' should not be duplicated in the list

Scenario: C. Add a Skill with empty fields
  Given I log into Localhost portal
  When I navigate to the Profile's Skills section
  And I try to add the Skill ' ' with level ' '
  Then The Skill ' ' should not be added

Scenario: D. Add a Skill with only numbers
  Given I log into Localhost portal
  When I navigate to the Profile's Skills section
  And I try to add the Skill '0987' with level 'Intermediate'
  Then The Skill '0987' should be added

Scenario: E. Add a Skill with special characters
  Given I log into Localhost portal
  When I navigate to the Profile's Skills section
  And I try to add the Skill '&^#@)}:' with level 'Beginner'
  Then The Skill '&^#@)}:' should be added

Scenario: F. Add a Skill with a very long string
  Given I log into Localhost portal
  When I navigate to the Profile's Skills section
  And I try to add the Skill 'aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa' with level 'Expert'
  Then The Skill 'aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa' should be added

Scenario: G. Edit a Skill
  Given I log into Localhost portal
  When I navigate to the Profile's Skills section
  And I edit the Skill 'Testing' to 'Automation' with level 'Expert'
  Then The Skill 'Automation' should appear in the list

Scenario: H. Delete a Skill
  Given I log into Localhost portal
  When I navigate to the Profile's Skills section
  And I delete the Skill 'Automation'
  Then The Skill 'Automation' should not appear in the list
