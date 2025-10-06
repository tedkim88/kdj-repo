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
  className="relative min-h-screen flex flex-col md:flex-row items-center justify-center px-8 py-16 
             bg-gradient-to-b from-black via-violet-500 to-black text-white"
>
      {/* 입자 배경 */}
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
            shape: { type: ["circle", "triangle", "star"] },
            size: { value: { min: 2, max: 3 } },
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
      <div className="md:w-1/2 md:pl-12 relative z-10">
        <h2 className="text-4xl font-bold mb-4">About Me</h2>
        <p className="text-lg md:text-xl mb-4">
          Hi, I'm Daejin Kim, a passionate Fullstack Developer with experience
          in React, .NET, Express, Next.js and many others. I love building modern, responsive web applications
          and learning new backend & frontend technologies and architectures like clean architecture.
        </p>
        <p className="text-lg md:text-xl">
          I focus on writing clean, maintainable code and creating user-friendly
          interfaces. When I'm not coding, I enjoy exploring a variety of projects that I can find on the Internet and
          experimenting with creative projects that can help solve real-world problems.
          
        </p>
      </div>
    </section>
  );
}

export default About;
