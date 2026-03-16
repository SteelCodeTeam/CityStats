# CityStats

**CityStats** is a mod that shows some city stats in real time while you play.

## What it shows

### Citizen count
Counts all humans currently moving with a navigation target set.

**Excluded:**
- Animals
- Wildlife

### Service Vehicles
Counts any moving vehicle with a service type assigned, such as:
- Ambulances
- Fire trucks
- Police vehicles
- Other service vehicles

**Excluded:**
- Parked service vehicles

### Parked Vehicles
Counts any parked car, including:
- Service vehicles not currently in use
- Cars stored in home garages
- Cars parked on streets
- Cars parked in parking lots

### Moving Vehicles
Counts personal vehicles currently moving with a navigation target set.

**Excluded:**
- Parked vehicles
- Service vehicles
- Bicycles

For example, vehicles driving on the main highway without entering the city may still be counted as moving vehicles.

## Important notes

As you can see, these numbers are **not perfectly exact**, but they are a **good real-time estimate** of how many entities are active in the game.

I recommend testing the mod in a **new city** with only **2 or 3 basic buildings** first. That makes it easier to compare the values with what is actually happening and better understand how the counters behave.

I have verified that there is **no difference in the citizen count**, but the number of vehicles on the road can vary slightly. For example, it may not count the first delivery vehicles when a business opens.

## Feedback and bug reports

If you find a bug, please keep in mind that **this is my first mod**.

If you have ideas for new functionality, or if you receive an error message, please contact me through GitHub or Discord.

- **Discord:** RuDaHee
