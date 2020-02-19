Feature: TechGenixLogin
	

@smoke
Scenario: Navigation to TechGenix Portal
	Given I navigate to TechGenix Portal
	When I navigate to THE T-SUITE under PODCAST Section
	Then I verify Feature Product label is Displayed
	And I exit from the Application

@Regression
Scenario: Navigation to Tutorials Portal
	Given I navigate to TechGenix Portal
	When I navigate to THE Tutorials Tab
	Then I verify Recommended Header is Displayed
	And I exit from the Application
