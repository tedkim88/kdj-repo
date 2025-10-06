import React from "react";
import Particles from "react-tsparticles";
import { loadFull } from "tsparticles";

function Skills() {
  const particlesInit = async (main) => {
    await loadFull(main);
  };

  const skillCategories = [
  {
    title: "Web Development",
    skills: [
      "HTML, Javascript, Typescript, CSS(SASS), PHP, Next.js",
      "React with ASP.Net Core or Express, Blazor WebApp with C# (Full stack experience)",
      "Oracle DB with Oracle Form"
    ],
  },
  {
    title: "Database & Backend",
    skills: [
      "MS SQL Server, Oracle, MySQL, MongoDB, Firestore",
      "ASP.Net Core, Express, Node.js, EntityFrameWorkCore, Prisma, Mongoose",
      "RESTful APIs, Firebase Auth & DB(Firestore), JWT, bcrypt"
    ],
  },
  {
    title: "DevOps",
    skills: ["Docker (WSL-Ubuntu)", "Bash Scripting", "Vim", "CI/CD Pipelines"],
  },
  {
    title: "Cloud",
    skills: ["Azure & AWS", "Deploy apps through Azure App Service & SQL DB"],
  },
  {
    title: "Data Analytics",
    skills: ["Data warehousing", "ETL, SSIS, SSAS, SSRS", "Excel"],
  },
  {
    title: "Self-Learning & Testing",
    skills: ["Angular", "Python", "Power BI", "xUnit Testing with C#"],
  },
  {
    title: "Version Control & Tools",
    skills: ["Git & Github", "Axios", "React Query", "Zustand", "Postman", "DaisyUI", "Shadcn UI", "Material UI", "Chakra UI", "Tailwind CSS", "Bootstrap", "MudBlazor"],
  },
];


  return (
    <section
      id="skills"
      className="relative min-h-screen px-8 py-4 
             bg-gradient-to-b to-black via-emerald-500 from-black text-950 
             text-white overflow-hidden flex flex-col items-center justify-center"
    >
      {/* 배경 입자 */}
      <Particles
        id="tsparticles-skills"
        init={particlesInit}
        options={{
          fullScreen: { enable: false },
          background: { color: { value: "transparent" } },
          particles: {
            number: { value: 25 },
            color: { value: ["#f5f3f4", "#c7d2fe", "#a78bfa", "#f472b6"] },
            shape: { type: ["circle", "triangle"] },
            size: { value: { min: 2, max: 4 } },
            move: { enable: true, speed: 1.5, outModes: "out" },
            links: { enable: true, distance: 130, color: "#ffffff", opacity: 0.2, width: 1 },
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
      <h2 className="text-4xl font-bold mb-12 text-center relative z-10">My Skills</h2>

      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 w-full max-w-6xl relative z-10">
        {skillCategories.map((category, idx) => (
        <div
  key={idx}
  className="bg-gray-900 bg-opacity-50 p-6 rounded-xl shadow-lg min-h-[280px] hover:scale-105 transition-transform flex flex-col"
>

            <h3 className="text-2xl font-semibold mb-4 text-yellow-200">{category.title}</h3>
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
