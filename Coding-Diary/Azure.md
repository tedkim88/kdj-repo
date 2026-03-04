\# Azure Core Concepts \& Resources



Comprehensive Azure reference covering key services and architecture patterns learned from:

\- AZ-900 (Fundamentals)

\- AZ-104 (Administrator)

\- AZ-204 (Developer)

\- AZ-305 (Solutions Architect)



---



\# 1. Compute Services



\## Virtual Machines (VM)

Infrastructure as a Service (IaaS) compute.



Features:

\- Full OS control

\- Custom images

\- Managed disks (I can add additional disc and detach it and re-attach it to another VM as well)

\- Extensions

\- Availability Sets / Zones



Use Cases:

\- Legacy apps

\- Full OS control needed

\- Lift-and-shift migration



---



\## Virtual Machine Scale Sets (VMSS)



Automatically scales VM instances. (I set up the scale rules, this is usually used for 'scaling VMs of same specs') 

(Minimum, Maximum instances can be set up)



Features:

\- Autoscaling

\- Load balanced

\- Stateless workloads



---



\## Azure App Service (I can choose a code base..or I can go for container as well, but I didn't use container for App service)



PaaS web hosting platform.



Supports:

\- Web Apps

\- API Apps

\- Mobile Apps

\- WebJobs



Features:

\- Auto scaling

\- Deployment slots  (with this I can have both Dev and Prod servers running, and easily swap). 

\- Built-in authentication

\- CI/CD integration



---



\## Azure Functions (With Event Grid Topic, I can designate Az function as a subscriber of a specific event, and when triggered, AZ function executes the codes I write)

Consumption based payment model is really cheap.



Serverless event-driven compute.



Triggers:

\- HTTP

\- Timer

\- Queue

\- Event Grid

\- Blob



Plans:

\- Consumption

\- Premium

\- Dedicated



---



\## Azure Container Instances (ACI)



Lightweight container execution.



Characteristics:

\- Fast startup

\- No orchestration

\- Pay-per-second billing



---



\## Azure Container Apps (Advanced version of using AZ containers, but I didn't try this much, maybe later)



Serverless container platform.



Features:

\- Kubernetes-based

\- Dapr support

\- Microservices architecture

\- Auto scaling (KEDA)



---



\## Azure Kubernetes Service (AKS)



Managed Kubernetes cluster.



Features:

\- Container orchestration

\- Auto scaling

\- Rolling updates

\- Helm support



Use Cases:

\- Microservices

\- High-scale applications



---



\# 2. Storage Services



\## Blob Storage



Object storage for unstructured data.



Tiers:

\- Hot  (for frequently used data) it's not for long-term storage . expensive for storage. cheap for data retrieval

\- Cool at least save 30 days rule

\- Cold  at least save 90 days rule

\- Archive This one is good for long-term storage which doesn't need data retrieval. it takes time to retrieve data.



Blob Types:

\- Block Blob : only used this one

\- Append Blob

\- Page Blob



---



\## Azure Files (This is like a ...google drive system? I can upload files and make them synced with my local)

(I needed to install something like file share agent on local..and/or? opening up 445 ports for file share, which is not safe, but there was another way)

(I may need to practice later again)



Cloud-based file shares.



Protocols:

\- SMB

\- NFS



Use Cases:

\- Lift-and-shift file servers



---



\## Queue Storage



Message queue for async processing.

(For Queue, or Service Bus, Event Hub or things like that, I need to come up with the perfectly fit scenarios to use each of them.)
like Service Bus: Transaction, business, safe model

theoretically, Queue, Message Goes to Queue, and subscriber opens the message and while opening the message, other subscribers cannot see the menu

probably to prevent duplicate executions. I need to try this in a real app with the right scenario.



Use Cases:

\- Background processing

\- Decoupled architecture



---



\## Table Storage (Only know this is NOSQL based storage, I haven't used this)



NoSQL key-value storage.



---



\# 3. Databases



\## Azure SQL Database

(it automatically saves its own data to other areas(?) just in case), but if I want to I can use backup service for this one too

(security -- concept of TDE(DB manager can see) encryption, and Always Encrypt (This one probably needs Keyvault for opening?)

(There was something called dynamic masking, like hiding some specific private information in a column)

Managed relational database.



Features:

\- Elastic pools  (Cost-effective, one pool has many databases and they share the resources as needed)

\- Automatic backups

\- Geo-replication



---



\## Azure Cosmos DB



Globally distributed NoSQL database.



Features:

\- Multi-region replication

\- Multi-model API



APIs:

\- SQL API

\- MongoDB

\- Cassandra

\- Table

\- Gremlin



Consistency Models:

\- Strong

\- Bounded Staleness

\- Session

\- Consistent Prefix

\- Eventual



---



\## Azure Database for PostgreSQL / MySQL



Managed open-source database services.



---



\# 4. Networking



\## Virtual Network (VNet)

(the mostimportant thing to remember is different Vnets cannot communicate with each other without Vnet peering.

(for peering the vnets should not have any overlapping IP address range CIDR(??)).

after peering I could switch back and forth between different vnets with no difficult using SSH command (different Vnets had different VMs)



Also, should remember the concept of IP address range(?) and Subnets

NIC(Private IP address--network connector)

and NSG(Network security group). 

Through NSG I could set up inbound and outbound rules to block or allow access.

Lower priority numbers have higher powers.



Logical isolated network.



Features:

\- Subnets

\- NSG

\- Routing

(The concept of Routing Table was also important)

if Subnet A has routing table B

A follows B for navigating



---



\## VNet Peering



Private connectivity between VNets.



Types:

\- Regional

\- Global



---



\## Public Endpoint

Accessible via public internet.



---



\## Private Endpoint

Private IP access via VNet.



If a personal user(at home) wants to access Vnet, there should be some special approaches to take.

VPN(Virtual network gateway) (point to site)

VPN Site to Site (Company network to Azure)



For making such virtual gateway, its own subnet should be made first.



---



\## Service Endpoint



Secure access to Azure services from VNet.

(This is more like..allow access from selected Azure networks(Vnet).)

(still using the internet). Somehow this is different from private endpoint..



---



\## VPN Gateway



Encrypted tunnel between:



\- On-premises

\- Azure VNet



Types:

\- Site-to-Site

\- Point-to-Site



---



\## ExpressRoute



Private dedicated connection to Azure.

This is not through the internet. fast and secure but expensive. 



Benefits:

\- Lower latency

\- Higher security

\- Predictable performance



---



\## DNS



\### Azure Public DNS

Internet-facing DNS hosting.



\### Azure Private DNS

Internal VNet name resolution.



---



\# 5. High Availability



\## Availability Sets. (dividing In the same building(datacenter))



Protect against hardware failure.



Components:

\- Fault domains

\- Update domains



---



\## Availability Zones (dividing data into different data centers) .basically a signle zone is a datacenter



Physically separate datacenters within region.



---



\# 6. Data Redundancy



\## LRS



3 copies in single datacenter.



---



\## ZRS



Replicated across availability zones.



---



\## GRS

(A Region can have multiple zones..need to remember this concept)

Cross-region replication.



---



\## RA-GRS

(Probably because secondary region cannot read data from primary region??, so need this setup?)

Read access to secondary region.



---



\# 7. Identity \& Security



\## Azure Entra ID (Azure AD)

(Tenant)

Identity provider for cloud authentication.



Supports:

\- OAuth

\- OpenID

\- SAML



---



\## RBAC (Role-Based Access Control)



Controls access to Azure resources.



Scopes:

\- Management Group

\- Subscription

\- Resource Group

\- Resource



---



\## Managed Identity



Secure service-to-service authentication.



Types:

\- System Assigned

\- User Assigned



---



\## Conditional Access



Policy-based access control.



Examples:

\- MFA enforcement

\- Location-based restrictions



---



\# 8. Integration \& Messaging



\## Azure Event Grid



Event routing service.



Use cases:

\- Resource event notifications



---



\## Azure Event Hub



Big data streaming platform.



Use cases:

\- Telemetry ingestion

\- IoT data streams



---



\## Azure Service Bus



Enterprise messaging.



Features:

\- Topics

\- Queues

\- Dead-lettering



---



\# 9. Monitoring



\## Azure Monitor



Platform monitoring solution.



Includes:

\- Metrics

\- Logs



---



\## Log Analytics



Query logs using KQL.



---



\## Application Insights



Application performance monitoring.



Features:

\- Request tracking

\- Dependency tracking

\- Exception logging



---



\# 10. DevOps \& Deployment



\## ARM Templates



Declarative infrastructure deployment.



---



\## Bicep



Simplified ARM template language.



---



\## Azure DevOps



CI/CD platform.



Components:

\- Pipelines

\- Repos

\- Boards

\- Artifacts



---



\## GitHub Actions



Workflow automation for CI/CD.



---



\# 11. Architecture Patterns



\## Hub and Spoke



Centralized network architecture.



Hub:

\- Shared services

\- Firewall

\- VPN



Spoke:

\- Application VNets



---



\## Virtual WAN



Large-scale global networking.



Supports:

\- Hub-to-Hub connectivity

\- Branch connectivity



---



\# 12. Security Services



\## Azure Key Vault



Secure secrets management.



Stores:

\- Keys

\- Secrets

\- Certificates



---



\## Microsoft Defender for Cloud



Cloud security posture management.



---



\## Azure Firewall



Stateful network security service.



---



\# 13. Governance



\## Azure Policy



Enforces organizational rules.



---



\## Azure Blueprint



Deploy compliant environments.



---



\## Management Groups



Organize subscriptions.



---



\# 14. Migration



\## Azure Migrate



Migration assessment tool.



---



\## Azure Site Recovery



Disaster recovery solution.



---

