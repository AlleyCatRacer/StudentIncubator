# Student Incubator
Written as a 3<sup>rd</sup> semester project in autumn of 2021 for software engineering at VIA university college[^via], campus Horsens, DK.  
The project idea is a website based game with minimal graphics. The goal of the game is to create an avatar which is a college student and keeping it alive throughout a semester.

## The Game
Before being able to play the game you must create an account or login with an existing account and create an avatar. An avatar starts out with 4 status bars, each starts at the maximum of 100 points. If any of them drops to zero, your avatar dies, i.e. is deleted from the database and you are presented with the cause of death. The status bars represent different aspects of college life:
- Academic
  - Current academic performance
- Financial
  - Available funds
- Health
  - Physical health
- Social
  - Mental health

Playing the game entails selecting an avatar to use and then performing actions such as sleeping, studying, working, etc. Each action has a defined effect on the status bars. In order to win the game the avatar must keep all status bars above zero until they finish the semester, i.e. run out of time blocks. A semester consists of 80 time blocks and an actions' duration also varies by type. This sounds simple enough, which means it's not realistic.  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;In order to better mimic the unpredictability of life, there are random events that have a chance to occur every time there is an attempt to perform an action. When a random event occurs, the original attempted action is not performed. The random events include, but are not limited to: bumping into an old friend and having a pleasant chat, having your bike stole, getting laid and getting hit by a car. Understandably, these events can have both positive and negative effects on multiple statuses.

## Development
The project was developed using the iterative Agile approach. We had freedom to choose our own methodology for this semester and we agreed on a Scrum/Kanban hybrid. This consisted of the addition of a Kanban board using Jira to manage the flow and work distribution as well as modifying scrum ceremonies to better suit our sprint lenght and schedule.  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;The requirements imposed by the course were to create a 3-tiered heterogeneous system that utilizes at least two different communication technologies and includes server-to-server communication.  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Our three tiers are Client, Logic Server and Data Server. The first two tiers are written in C# using JetBrains Rider. The Client tier is a web client with a UI using Blazor. Communication between the Logic Server and Client is a RESTful web API. The third tier is in Java written in IntelliJ IDEA and handles data manipulation and database access.  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;In order to keep the Client as responive as possible we use the BAEC principle, requiring asynchronous communication which we implement in the form of RabbitMQ between the Logic and Data Server. The database itself is written in PostgreSQL using DataGrip.

## Development Team
The semester project group consisted of 4 people:

- Aldís Eir Hansen, AKA Allie [^0]
- Lili Zsuzsanna Tóth, AKA Lil [^1]
- Lukas Reimantas Tiuninas, AKA Rei [^2]
- Joseph Carroll, AKA Jody [^3]

### Author Reference
Base code DatabaseHelper.java and DataMapper.java courtesy of Ole Ildsgaard Hougaard [^4].  
Base code for Authenticator.cs courtesy of Troels Mortensen [^5].

[^via]: https://via.dk
[^0]: https://github.com/AlleyCatRacer
[^1]: https://github.com/tothlilizs
[^2]: https://github.com/SkyKalazar
[^3]: https://github.com/carrolljody
[^4]: https://github.com/olehougaard
[^5]: https://github.com/TroelsMortensen
