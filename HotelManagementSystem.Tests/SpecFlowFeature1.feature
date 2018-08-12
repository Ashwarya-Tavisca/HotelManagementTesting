Feature: Add hotel
	In order to simulate hotel management system
	As api consumer
	I want to be able to add hotel,get hotel details and book hotel

@AddHotel
Scenario Outline: User adds hotel in database by providing valid inputs
	Given User provided valid Id '<id>'  and '<name>'for hotel 
	And Use has added required details for hotel
	When User calls AddHotel api
	Then Hotel with name '<name>' should be present in the response
Examples: 
| id | name   |
| 1  | hotel1 |
| 2  | hotel2 |
| 3  | hotel3 |

@GetHotelById
Scenario Outline: User gets the details of the hotel from database by providing valid inputs
	Given User provided valid Id '<id>'  and '<name>'for hotel 
	And Use has added required details for hotel
	And User has called AddHotel api
	And user has provided a valid hotel id '<id>' to be retrieved
	When user calls GetHotelById api
	Then Details of Hotel with id '<id>'  should be present in the response
Examples:
| id | name   |
| 9  | hotel9 |

@GetAllHotels
Scenario: User gets the details of all the hotels.
    Given User provided valid Id '6'  and 'hotel6'for hotel 
	And Use has added required details for hotel
	And User has called AddHotel api
	And User provided valid Id '7'  and 'hotel7'for hotel 
	And Use has added required details for hotel
	And User has called AddHotel api
	And User provided valid Id '8'  and 'hotel8'for hotel 
	And Use has added required details for hotel
	And User has called AddHotel api
	When User calls GetAllHotels api
	Then Hotel with names 'hotel6','hotel7','hotel8' should be present in the response

