# Weigh

Description: Simple weight loss app


TODO:

	General
		Add google docs backend or way to backup
		Add a way to know how on or off track you are to meet goal
		~~Add Measurements for Waist, maybe more
		~~Add extensions for imperial/metric changes
		~~Find out maths to meet goal in timeline

	Initial Setup Page & Settings Page
		Add notes to entries
		Add Labels to make it clear what you're editing
		~~Disable Editing
			~~Create Switch which enables/disables all editing
			~~Disable Editing Weight from Settings
		~~Add messaging from SettingsPage to MainPage
			~~Goal Weight/Date -> Main Page
		
		~~Change entry placeholders based on imperial/metric
		~Add entry for goal weight, and timeline

	Graphs Page
		Add ability to click listview items
			Add view for item details
		Add area colors for bmi
		~~Add change from last entry
		~~Add day to listview
		

	Main Page
		Handle case of goal met (new goal, or show maintain caloric intake)
		~~Add Card styling
		~~Add goal weight & distance to it
		~~Add current weight
		~~Add time left to goal timeline


Sources:

	Min caloric intake (http://www.acsm.org/about-acsm/media-room/acsm-in-the-news/2011/08/01/metabolism-is-modifiable-with-the-right-lifestyle-changes)
	BMI categories (https://www.nhlbi.nih.gov/health/educational/lose_wt/BMI/bmicalc.htm)
	BMR equations (https://en.wikipedia.org/wiki/Harris%E2%80%93Benedict_equation)
	Caloric defecit numbers (http://www.exercise4weightloss.com/bmr-calculator.html)


Scratch math zone:

	Weight = 237lb
	Height = 5'10"
	Goal Weight = 190lb
	Goal Date = 180 days away
	Gender = Male
	Minimum Caloric intake for males = 1800 calories
	Caloric defecit to lose 1lb/wk = 500 calories
	BMT = 3063 calories
	Weight left = (Weight - Goal Weight) = 47lb

	Required Weight Loss/day to meet goal = (Weight left / Goal Date) = 0.2611111111111111 lb/day, 1.827777777777778 lb/wk
	Total caloric defecit needed = 500 * 1.827777 = 913.8888888888889
	Recommended Daily Caloric Intake = 3063 - 913.8888888888889 = 2149.111111111111
