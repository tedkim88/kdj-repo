// src/components/About.jsx
import React from "react";
import Particles from "react-tsparticles";
import { loadFull } from "tsparticles";

const particlesInit = async (main) => {
  await loadFull(main);
};

function About() {
  return (
    <section
      id="about"
      className="relative min-h-screen flex flex-col md:flex-row items-center justify-center px-2 py-16 
             bg-gradient-to-b from-black via-sky-700 to-black text-white"
    >
      
      <Particles
        id="tsparticles-about"
        init={particlesInit}
        options={{
          fullScreen: { enable: false },
          background: { color: { value: "transparent" } },
          particles: {
            number: { value: 10 },
            color: {
              value: ["#ffffff", "#f5f3f4", "#c7d2fe", "#a78bfa", "#f472b6"],
            },
            shape: { type: ["circle", "triangle"] },
            size: { value: { min: 1, max: 2 } },
            move: { enable: true, speed: 2, outModes: "out" },
            links: {
              enable: true,
              distance: 150,
              color: "#ffffff",
              opacity: 0.3,
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

      {/* 왼쪽: 프로필 이미지 */}
      <div className="md:w-1/3 flex justify-center mb-8 md:mb-0 relative z-10">
        <img
          src="/daejin.jpg" // public 폴더에 넣은 사진
          alt="Daejin Kim"
          className="w-64 h-64 rounded-full border-4 border-white shadow-lg"
        />
      </div>

      {/* 오른쪽: 소개 텍스트 */}
      <div className="md:w-1/2 md:pl-12 relative z-10 font-oswald">
        <h2 className="text-4xl font-bold mb-4">About Me</h2>
        <p className="text-lg md:text-xl mb-4">
          Hi, I'm Daejin Kim, a passionate Fullstack Developer with experience
          in{" "}
          <span className="text-yellow-300 font-bold">
            React, Next.js, .NET, ASP .Net Core Web API, Express, and many
            others.{" "}
          </span>{" "}
          I have done many portfolios using{" "}
          <span className="text-yellow-300 font-bold">
            {" "}
            Restful APIs with clean architecture{" "}
          </span>{" "}
          and used{" "}
          <span className="text-yellow-300 font-bold">
            Web socket(socket.io) for real-time chat{" "}
          </span>{" "}
          in my recent project. More recently, I'm studying{" "}
          <span className="text-yellow-300 font-bold">
            {" "}
            AI integration with the concept of 'Agentic AI'.
          </span>
        </p>

        <p className="text-lg md:text-xl mb-4">
          <span className="text-yellow-300 font-bold">
            My biggest strength is understanding the entire product lifecycle —
            from data modeling and backend API design to frontend UI & UX and
            deployment.
          </span>
          <br />
          <br />I usually begin by identifying the core data of a real-world
          problem, designing the entities and database relations (usually with
          dotnet migrations), and implementing backend APIs with proper testing
          through Postman. I then build the frontend simultaneously to ensure
          that the best user experience drives API design.
          <span className="text-yellow-300 font-bold">
            {" "}
            I try to keep detailed notes of features and bugs, and I automate
            repetitive tasks using bash scripts to improve development
            efficiency.
          </span>
        </p>

        <p className="text-lg md:text-xl mb-4">
          <span className="text-yellow-300 font-bold">
            For version control, I primarily use Git with GitHub. Everytime I
            need a new feature, I create a new branch from the base one, and
            after testing and code review, I merge it back to the base branch.{" "}
          </span>
          When I collaborate with others on GitHub, I make sure to assign each
          one to work on different pages or components to avoid conflicts.
        </p>

        <p className="text-lg md:text-xl">
          I focus on writing clean, maintainable code and creating user-friendly
          interfaces. When I'm not coding, I enjoy exploring a variety of
          projects that I can find on the Internet and experimenting with
          creative projects that can help solve real-world problems.
        </p>
      </div>
    </section>
  );
}

export default About;
