# KDJ Portfolio

Welcome to my portfolio repository! Below are the links of my portfolio

1. **Game-Hub (Full Stack React-App) (the latest one) 2025/July/16**  
(Front Deployed to Netlify & Backend Deployed to Render.com)
https://rococo-chimera-697176.netlify.app/

Typescript(front) + Express(backend) + MongoDB(NOSQL) + jwtAuth + Websocket(Real-time chat) + Tailwind CSS + Daisy UI + ....

This is a website where people can get game recommendations by platform.  
Users can search for game info by platforms and/or genres.  
Not only that, they are going to be able to share opinions in a board,  
get game information in detail, chat with signed-in users(web-socket) in real-time to find game partners.


   ![image](https://github.com/user-attachments/assets/fffe26ac-f6e9-45cc-8125-2030a8e8094f)

![image](https://github.com/user-attachments/assets/08d21800-f790-4411-bdee-20e42d983a15)


2. **OLTP Shopping (C# / BlazorServer / EF Core)**  (Deployed through Azure)  
   https://dmit2018project20250507014855-g7b8hjdbhyb9dbht.canadacentral-01.azurewebsites.net/  

3. **Rental Management System (C# / BlazorServer / EF Core)**  (Deployed through Azure)  
   https://rmsapp20250507022951-d3geewamgpd5a6ah.canadacentral-01.azurewebsites.net/RentalList  

4. **Word-Smith Personal JS project (JavaSript & Firebase)** (Deployed through Render.com)  
   https://wordsmith-finished.onrender.com

5. **Modal & Form (CSS)** (Deployed through Netlify)  
   https://graceful-gecko-22ac0e.netlify.app/  
    
6. **Responsive Design (CSS)**  (Deployed through Netlify)  
   https://jolly-lamington-d3ce51.netlify.app/



   
   
    
----------------------------------------------  

Project Introduction

## 1. **WordSmith** (JavaScript-based Web App)
**Description**:  
WordSmith is my personal JavaScript-based web application designed to help users learn vocabulary and manage their own word lists using Firebase Database. The app includes a quiz feature, allowing users to search for words, view their meanings, add words to a personal list, and then quiz themselves on the saved words.

**pre-made ID : 123456@gmail.com**
**pre-set password : 123456**

![image](https://github.com/user-attachments/assets/f3cc0235-7044-46e1-9d89-52a49d534396)
![image](https://github.com/user-attachments/assets/ad411246-74bf-4829-b710-09e348b9cf91)



**Features**:
- **Search Functionality**: Search for words and view their meanings.
- **Add to Word List**: Add words to a temporary list before saving to DB
- **Quiz**: Review words through quizzes based on your saved lists from firebase DB.
- **Firebase Auth** : Email authentication is used for signing in, using Firebase Auth.
- **FireStore Database** : NOSQL database has been used to save data coming from users(serverless)

**Challenges**:
- **Didn't spend time for design: it was just for understanding and applying functionality**
- **API Limitations**: The free word API may not provide information for certain words.
- **(DEPRECATED now) Board Functionality**: Initially planned for Q&A about word usage, but it's currently used just for learning.
- **Duplicate Data**: I used a Set object to avoid duplicates in the quiz, which was successful, but the DB may still contain duplicates due to how the lists are saved. It could be improved later by manipulating data BEFORE saving.
- **First Trial** -- With the benefit of hindsight, I could've done things in a much simpler way, by planning ahead about the functionality I was going to use, and data structures I was going to use. When I first started this project, I only thought about the purpose of the web app--to help learn English words.
  
  **TakeAway** : I'm going to plan ahead about the functionality and data structure so that the codes can be as simple but efficient as possible.

**Refactoring finished** 2025/May/08

----------------------------------------------  

## 2. **OLTP Shopping / Rental Management System** (C# & Blazor & EntityFrameworkCore / SQL))
**Description**: 
**Note** I was responsible for **'SalesSystem'**, Sales refund related page for this team project. Each one had their own part.
**POSystem is not my part.**

These projects demonstrate my understanding of C# and Blazor. They are based on rough guidelines provided in my C# classes at NAIT. The applications are designed around Online Transaction Processing (OLTP) and rental management systems. These apps utilize Entity Framework Core for data management, SQL queries to extract data, and Blazor for rendering the front end.

**Features**:
- **Entity Framework Core**: Data handling and database management.
- **Blazor**: Front-end rendering and UI interactions.
- **SQL Queries**: Extracting and presenting data from a local SSMS database.

![image](https://github.com/user-attachments/assets/473b9f8f-ddb2-47ba-bd51-3ab143616a71)
![image](https://github.com/user-attachments/assets/3604d27f-fadb-413d-a63a-50663269f6de)
![image](https://github.com/user-attachments/assets/bd1422d5-cb21-4279-8671-bea8020c6a52)
![image](https://github.com/user-attachments/assets/9ac14bec-8e6e-4990-9959-cb72fe439f6a)
![image](https://github.com/user-attachments/assets/cb16e5af-a97a-4931-ac00-0eee74b45ba8)
![image](https://github.com/user-attachments/assets/8a9680d9-0ffb-4660-93dc-19f6bbe90c74)
![image](https://github.com/user-attachments/assets/971d5216-6479-4085-878e-1b59e2de21a6)


---

## 3. **WeatherInfo Simple Web App**  
**Description**:  
This is not a full-fledged project but rather my first attempt at using a free API to create a simple web application for tracking weather history.


**Note**:  
These projects are works in progress or practice attempts. The code structure, use of services, and data management techniques can be evaluated without the database files. I'll be uploading videos for some projects to demonstrate their functionality.

---

## 4. **Design-shopping-mall (MODAL & Form)**  
**Description**:  
Through this project, I learned how to pop-up modal window to encourage customers to sign up for the site or submit their data. 
This site has functionality for horizontally displaying products through scroll.
This website, when it is in rendered in mobile(small) size, has a hamburger button.  

**I do know how to show submenus that pop up after clicking hamburger nav button but for this project, I haven't done the coding for that.**  
Submenu functions are shown in the project number 5 (responsive and navigation).

The purpose of this project for me was to practice modal and form.
![image](https://github.com/user-attachments/assets/58204c41-088f-4c87-9d68-df90cde06367)


---  

## 5. **Design-responsive and navigation** 
**Description**:  
Doing this project, I got to learn how and what to do with position absolute, relative, getting more used to flex and its usage for aligning items even when something doesn't need to be flexed.
I made toggle-navigation as well using simple JS commands, and this navigation display changes depending on the viewport size, using media queries.
Using the concept of inner-container to maintain the content in the center, but surrounding it with another container box which doesn't have max-width so that it can have a stretching back ground was also another takeaway.
I used svg minifier to clean up the svg codes, and squoosh web app to optimize images(changing extension to webp) for better display and sizes.

![image](https://github.com/user-attachments/assets/ab07bdd1-a20e-4db0-973c-42ef56e7eb5a)
![image](https://github.com/user-attachments/assets/99749189-8fb8-4811-8650-50eb611330c6)
![image](https://github.com/user-attachments/assets/4d2a8353-377f-4ac3-8ec8-9a74ea377569)



