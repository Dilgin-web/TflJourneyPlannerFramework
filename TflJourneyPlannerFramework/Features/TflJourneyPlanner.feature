@JourneyPlanner
Feature: TFL Journey Planner Widget

check the journey planner widget

 
Scenario: Plan and verify valid journey from Leicester Square Underground Station to Covent Garden Underground Station

Given the user is on the TFL journey planner page
When  the user enters "<StartPoint>" as the starting point
And   the user enters "<Destination>" as the destination
And   the user selects a journey plan
Then  the user should see walking and cycling time for the journey

 Examples:
    | StartPoint | Destination    |
    | Leicester  | Covent Garden  |


Scenario: Edit journey preferences to select least walking route
    Given a valid journey has been planned using "<StartPoint>" and "<Destination>"
    When  the user selects "Edit preferences"
    And  the user chooses the route with least walking
    And  the user updates the journey
    Then the journey time should reflect the updated preference

Examples:
    | StartPoint | Destination   |
    | Leicester  | Covent Garden |

Scenario: View journey details for complete access information at Covent Garden
    Given a valid journey has been planned using "<StartPoint>" and "<Destination>"
    When  the user clicks View Details Button
    Then  the user should see complete access information at Covent Garden Underground Station

    Examples:
    | StartPoint  | Destination   |
    | Leicester  | Covent Garden  |
Scenario: Attempt to plan a journey with an invalid location
    Given the user is on the TFL journey planner page
    When the user enters "<StartPoint>" as the starting point
    And  the user enters "<Destination>" as the destination
    And  the user tries to plan the journey
    Then the widget should not provide journey results

    Examples:
    | StartPoint | Destination    |
    | Isle of Skye  | Covent Garden  |

 Scenario: Attempt to plan a journey without entering locations
    Given the user is on the TFL journey planner page
    When  the user tries to plan a journey without entering any locations
    Then  the widget should not allow the journey to be planned