import React from "react";
import Particles from "react-tsparticles";
import { loadFull } from "tsparticles";

function ProblemSolving() {
  const particlesInit = async (main) => {
    await loadFull(main);
  };

  const problemSolvingCategories = [
    {
      title: "Logging & Monitoring",
      
      skills: `• Mission: Securely maintain logs of specific HTTP requests.
    • Process: HTTP Request -> Event Grid Topic -> Azure Function App (Subscriber) -> Blob Storage (save logs).
    • Outcome: Offloaded logging overhead from the main thread, ensuring system resilience and data integrity without sacrificing API performance.`,
    },
    {
      title: "Azure API Management for rate limiting",
      skills: `• Mission: Protect backend resources from DDoS attacks and excessive traffic by implementing a robust rate-limiting layer.
    • Process: Designed inbound policy with Azure API management for rate-limiting. Interaction between resources done through Managed Identity and Key Vault for security.
    • Outcome: Successfully handled traffic spikes by returning HTTP 429 (Too Many Requests), ensuring 100% backend availability.`,
    },
    {
      title: "Standardized API Response",
      skills: `• Mission: Eliminate inconsistent API response schemas coming from backend.
    • Process: Implemented Generic Response Wrappers(dealing with business rules) & Global Exception Middleware in .NET(network-related unexpected error).
    • Outcome: Reduced debugging time and improved front-end productivity by providing predictable data structures.`,
    },
    {
      title: `Bash script for Project Setup`,
      skills: `• Mission: Reduce repetitive project setup and environment configuration time.
    • Process: Developed Bash automation scripts for backend project setup and utilized docker-compose.yml for DB setup.
    • Outcome: Cut initial project setup time dramatically, allowing more focus on feature development and testing.`,
    },
    {
      title: "Azure Devops Pipeline",
      skills: `• Mission: Automating Azure Function App deployment using Azure DevOps.
    • Process: Utilized Azure Devops Classic Editor for pipeline configuration, managing artifacts and deployment stages.
    • Outcome: Streamlined deployment process, reducing manual errors and deployment time.`,
    },
    {
      title: "Role-Based Access Control (RBAC)",
      skills: `• Mission: Ensure each user only accesses authorized resources.
    • Process: Implemented strict RBAC, using both Clerk's middleware(from frontend) to validate user roles and token claims and roles(from backend).
    • Outcome: Enhanced application security and compliance with data protection standards.`,
    },
    {
      title: "Agentic AI Integration",
      skills: `• Mission: Use Agentic AI for students' english learning enhancement.
    • Process: Students Input their English sentences and receive consistent AI-driven feedback by ensuring fixed format of JSON response.
    Mistake data is automatically saved through MCP for later review and practice.
    • Outcome: Improved learning engagement and outcomes through interactive AI-driven practice sessions.`,
    },
  ];

  return (
    <section
      id="problem-solving"
      className="appear-animation relative min-h-screen px-8 py-4 
             bg-gradient-to-b to-black via-blue-400 from-black text-950 
             text-white overflow-hidden flex flex-col items-center justify-center pt-20"
    >
      {/* 배경 입자 */}
      <Particles
        id="tsparticles-skills"
        init={particlesInit}
        options={{
          fullScreen: { enable: false },
          background: { color: { value: "transparent" } },
          particles: {
            number: { value: 7 },
            color: { value: ["#f5f3f4", "#c7d2fe", "#a78bfa", "#f472b6"] },
            shape: { type: ["circle", "triangle"] },
            size: { value: { min: 1, max: 3 } },
            move: { enable: true, speed: 2, outModes: "out" },
            links: {
              enable: true,
              distance: 130,
              color: "#ffffff",
              opacity: 0.2,
              width: 1,
            },
          },
          interactivity: {
            events: {
              onHover: { enable: true, mode: "repulse" },
            },
          },
        }}
        className="absolute inset-0 z-0"
      />

      {/* 섹션 콘텐츠 */}

      <h2 className="text-4xl font-bold mb-12 text-center relative z-10">
        Problem Solving Experience
      </h2>

      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 w-full max-w-6xl relative z-10">
        {problemSolvingCategories.map((category, idx) => (
          <div
            key={idx}
            className="bg-gray-900 bg-opacity-80 p-6 rounded-xl shadow-xl min-h-[320px] hover:scale-105 transition-all border border-blue-500/30 flex flex-col justify-start"
          >
            <h3 className="text-2xl h-20 font-bold mb-6 text-yellow-400 border-b border-yellow-400/20 pb-2">
              {category.title}
            </h3>


            <p className="text-gray-200 text-base leading-7 whitespace-pre-line tracking-wide">
            
              {category.skills.split("\n").map((line, i) => {
                if (line.includes("• Mission:"))
                  return (
                    <span key={i} className="block mb-1">
                      <strong className="text-cyan-300">Mission:</strong>{" "}
                      {line.split("Mission:")[1]}
                    </span>
                  );
                if (line.includes("• Process:"))
                  return (
                    <span key={i} className="block mb-1">
                      <strong className="text-purple-300">Process:</strong>{" "}
                      {line.split("Process:")[1]}
                    </span>
                  );
                if (line.includes("• Outcome:"))
                  return (
                    <span
                      key={i}
                      className="block mt-2 pt-2"
                    >
                      <strong className="text-green-400">Outcome:</strong>{" "}
                      {line.split("Outcome:")[1]}
                    </span>
                  );
                return (
                  <span key={i} className="block">
                    {line}
                  </span>
                );
              })}
            </p>
          </div>
        ))}
      </div>
    </section>
  );
}

export default ProblemSolving;
