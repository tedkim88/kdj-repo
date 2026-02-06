import React from "react";
import Particles from "react-tsparticles";
import { loadFull } from "tsparticles";

function Skills() {
  const particlesInit = async (main) => {
    await loadFull(main);
  };

  const skillCategories = [
    {
      title: "Web Dev(Full Stack)",
      skills: [
        "React(Next.js) with ASP.Net Core(Web API), Express.js, FastAPI, Blazor WebApp with C# (Full stack)",
        "RESTful APIs, Socket.IO for real-time messaging",
        "Typescript, Javascript, CSS (Tailwind), PHP",
        "Mobile-React Native(Expo)",
        "Oracle DB with Oracle Form",
      ],
    },

    {
      title: "Database & Backend",
      skills: [
        "MS SQL Server || Oracle || PostgreSQL (Procedure, Function, Trigger), MySQL, MongoDB, Firestore",
        "ASP.Net Core Web API, Express, Node.js, EntityFrameWorkCore, Prisma, Mongoose",
        "JWT, Clerk Auth(Token management), Clerk Webhook, Firebase Auth & DB(Firestore), bcrypt-hashing",
        "xUnit Testing (FluentAssertions)",
      ],
    },
    {
      title: "Certificate (Earned)",
      skills: [
        "MS Azure Fundamentals AZ-900 (2025)",
        "MS Azure Developer Associate AZ-204 (2026)",
      ],
    },
    {
      title: "DevOps",
      skills: [
        "Azure DevOps (CI/CD Pipelines, Repos)",
        "(WSL)Bash Scripting for Automation",        
        "Kubernetes (learning)",
        "Github Actions CI/CD",
      ],
    },
    {
      title: "Cloud & Deployment",
      skills: [
        "Azure (Entra ID, Azure DevOps, Event Grid Topic, Function App, Storage, App Service, VM, SQL)",
        "AWS (IAM, RDS)",        
        "Fly.io, Render, Netlify, Vercel",
        "Cloud DB (Neon, MongoDB, Supabase)"
      ],
    },

    {
      title: "Data Analytics",
      skills: [
        "Data warehousing",
        "ETL, SSIS, SSAS, SSRS",
        "Excel",
        "Power BI",
      ],
    },
    {
      title: "Learning",
      skills: [
        "Java(Spring Boot) (learning)",
        "Kubernetes (learning)",
        // "Angular(learning)",
        "LLMs(RAG, MCP, Agentic AI)",
        "Cyber Security",
      ],
    },
    {
      title: "Version Control & Tools",
      skills: [
        "Git & Github",
        "React Query",
        "Axios",        
        "Zustand",
        "Postman",
        // "DaisyUI",
        "Shadcn UI",
        "Material UI",
        // "Chakra UI",
        "Tailwind CSS",
        // "Bootstrap",
        "MudBlazor",
      ],
    },
  ];

  return (
    <section
      id="skills"
      className="pt-10 relative min-h-screen px-8 py-4 
             bg-gradient-to-b to-black via-emerald-500 from-black text-950 
             text-white overflow-hidden flex flex-col items-center justify-center pb-20"
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
          My Tech Skills
        </h2>

        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 w-full max-w-6xl relative z-10">
          {skillCategories.map((category, idx) => (
            <div
              key={idx}
              className=" bg-gray-900 bg-opacity-50 p-6 rounded-xl shadow-lg min-h-[280px] hover:scale-105 transition-transform flex flex-col"
            >
              <h3 className="text-2xl font-semibold mb-4 text-yellow-200">
                {category.title}
              </h3>
              <ul className="list-disc list-inside space-y-2">
                {category.skills.map((skill, i) => (
                  <li key={i}>{skill}</li>
                ))}
              </ul>
            </div>
          ))}
        </div>

    </section>
  );
}

export default Skills;
