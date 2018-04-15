Feature: ConsoleRendering
	In order to confirm the application renders correctly
	As a developer
	I want to check if certain fields are visible as expected

@BehavioralTesting
Scenario: Select a Whole Row
	Given Crosses selects the whole First Row
	Then The Game should be over
	And The Crosses should be the winnner
