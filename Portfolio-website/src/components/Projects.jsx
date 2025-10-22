import React from "react";
import Particles from "react-tsparticles";
import { loadFull } from "tsparticles";

const particlesInit = async (main) => {
  await loadFull(main);
};

function Projects() {
  const projects = [
    {
      name: "Game Hub Website (Websocket Realtime)",
      description:
        "(Real-time chat through Websocket) FullStack with React(Typescript) and Node.js Backend(Express) with JWT Authentication (the latest one 2025-August).",
      github: "https://rococo-chimera-697176.netlify.app/",
      image: "/react-app.png",
    },

    {
      name: "AI Integration Project - Speak With Me (Deployed to vercel)",
      description: (
        <>
          WebSpeech API, Prompt engineering and OpenAI API integration to my
          personal app ( This is{" "}
          <span className="text-red-500">
            still in development -- as of 2025-Oct-22. Features will be added
            and bugs will be fixed.
          </span>
          ).
        </>
      ),

      github: "https://speak-with-me-git-main-daejin-kims-projects.vercel.app/",
      image: "/speak-with-me.png",
    },

    //나중에 여기 두번째로 AI 앱넣을거
    {
      name: "PHP Catalogue Project(Deployed through Render & AWS(Mysql DB)",
      description: (
        <>
          PHP Catalogue Project with auth, CRUD, image upload, pagination and
          advanced filter. (
          <span className="text-red-500">
            Available after Nov 1 free-tier db expiration
          </span>
          ).
        </>
      ),

      github: "https://php-catalogue-projcet.onrender.com/public/",
      image: "/php-screenshot.png",
    },

    {
      name: "Shopping App(BLL/DAL)",
      description: (
        <>
          OLTP C# backend and Blazor frontend using EF Core. BLL DAL concepts
          implemented. Deployed to Azure.{" "}
          <span style={{ color: "red", fontWeight: "bold" }}>
            {" "}
            (I'm using a free tier server so it is a bit slow. Style has not
            been applied to this app.)
          </span>{" "}
        </>
      ),
      github:
        "https://etoolsapp-gcd9bbh2fzc7c0fs.canadacentral-01.azurewebsites.net/SalesReturn/Shopping",
      image: "/oltp-screenshot.png",
    },
    {
      name: "Rental Management System ",
      description: (
        <>
          C# Backend / BlazorServer / EF Core / BLL / DAL (Deployed Azure).
          <span style={{ color: "red", fontWeight: "bold" }}>
            {" "}
            (I'm using a free tier server so it is a bit slow. Style has not
            been applied to this app.)
          </span>
        </>
      ),

      github:
        "https://rmsapp20251017134502-dqenh3gchmbchmb2.canadacentral-01.azurewebsites.net/",
      image: "/rental-app.png",
    },

    {
      name: "Modal & Form (CSS) (Deployed through Netlify)",
      description: "Modal popup and Form (CSS) (Deployed through Netlify).",
      github: "https://graceful-gecko-22ac0e.netlify.app/",
      image: "/modal-design.png",
    },
    {
      name: "Basic Responsive design & Navbar (CSS) (Deployed through Netlify)",
      description:
        "Basic Responsive design (CSS) with responsive Navbar (Deployed through Netlify).",
      github: "https://jolly-lamington-d3ce51.netlify.app/",
      image: "/responsive.png",
    },
    {
      name: "My first beginner level self-project(Old one) Word-smith.",
      description:
        " Helps people learn,save English words with quiz. Javscript and Firebase used for serverless DB (Deployed through Render).",
      github: "https://wordsmith-finished.onrender.com",
      image: "/word-smith.png",
    },

    // 추가 프로젝트
  ];

  return (
    <section
      id="projects"
      className="relative min-h-screen px-8 py-16 
             bg-gradient-to-b to-gray-800 via-yellow-300 from-black text-950 
             text-white overflow-hidden flex flex-col items-center justify-center pt-50"
    >
      {/* 배경 입자 */}
      <Particles
        id="tsparticles-projects"
        init={particlesInit}
        options={{
          fullScreen: { enable: false },
          background: { color: { value: "transparent" } },
          particles: {
            number: { value: 20 },
            color: { value: ["#c7d2fe", "#a78bfa", "#f472b6"] },
            shape: { type: ["circle", "triangle"] },
            size: { value: { min: 2, max: 3 } },
            move: { enable: true, speed: 1.5, outModes: "out" },
            links: {
              enable: true,
              distance: 120,
              color: "#ffffff",
              opacity: 0.2,
              width: 1,
            },
          },
          interactivity: {
            events: {
              onHover: { enable: true, mode: "repulse" },
              onClick: { enable: false },
            },
          },
        }}
        className="absolute inset-0 z-0"
      />

      <h2 className="text-4xl font-bold mb-12 text-center relative z-10">
        My Projects
      </h2>
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8 relative z-10 w-full max-w-6xl">
        {projects.map((proj, idx) => (
          <div
            key={idx}
            className="bg-gray-900 bg-opacity-50 p-6 rounded-xl shadow-lg flex flex-col justify-between text-center hover:scale-105 hover:bg-indigo-700 transition-transform h-full"
          >
            <div>
              {proj.image && (
                <img
                  src={proj.image}
                  alt={proj.name}
                  className="w-full h-80 object-cover rounded-md mb-4"
                />
              )}
              <h3 className="text-2xl font-semibold mb-2">{proj.name}</h3>
              <p className="mb-4">{proj.description}</p>
            </div>
            <a
              href={proj.github}
              target="_blank"
              rel="noopener noreferrer"
              className="btn btn-primary mt-4"
            >
              Deployed Link
            </a>
          </div>
        ))}
      </div>
    </section>
  );
}

export default Projects;
