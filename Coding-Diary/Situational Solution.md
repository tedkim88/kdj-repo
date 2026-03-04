1. Azure Rate Limiting : Send requests to Azure API management(usually works between frontend and backend). before reaching backend,

it reaches API management, and I can set up lots of rules to determine whether to pass the request to the backend or not.

Here, I can use rate limiting option. Lots of other services as well. need to study.



2\. Logging Pipeline using Azure : Backend(Event Publisher) + AZ Event Grid Topic(Event management of sort) + AZ Function(Subscriber) + Storage(log stored by the codes in Azure function)



3\. Communication between AZ resources : Managed Identity(Resource A has managed identity) and when resource A accessing resource B, resource B needs to identify th identity of resource A and allows for access. If there's a key needed to access, the key can be saved in Key Vault.



4\. Real time chat(Socket.io) through express

the detailed code references in my private repository. 

need to understand that socket.io is a broadcaster. it doesn't go from user to user directly

we send a data with a specific event name, the server listens, and reacts accordingly. 

after receiving the data, the server can choose whether to send it to a specific user, or all users, or a certain group(group chat)



for successful execution, server needs to manage the connected users(online users) as an object, and updates the online user lists, when users log in and log out.

for details in my private repo



5\.















