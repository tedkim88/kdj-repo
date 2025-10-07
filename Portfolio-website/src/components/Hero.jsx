import React from "react";
import Particles from "react-tsparticles";
import { loadFull } from "tsparticles";

function Hero() {
  const particlesInit = async (main) => {
    await loadFull(main); // tsparticles 엔진 로드
  };

  return (
    <section className="relative min-h-[calc(100vh-64px)] flex flex-col items-center justify-center text-white text-center px-4 overflow-hidden bg-gradient-to-b from-black via-indigo-800 to-black">
      {/* 배경 입자 */}
      <Particles
        id="tsparticles"
        init={particlesInit}
        options={{
          fullScreen: { enable: false },
          background: { color: { value: "transparent" } },
          particles: {
            number: { value: 30 },
            color: {
              value: ["#ffffff", "#f5f3f4", "#c7d2fe", "#a78bfa", "#f472b6"],
            },

            shape: { type: ["circle", "triangle", "star"] },
            size: { value: { min: 2, max: 4 } },
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

      {/* 프로필 사진 */}
      <img
        src="/daejin.jpg"
        alt="My Photo"
        className="w-64 h-64 md:w-80 md:h-80 rounded-full border-4 border-white shadow-lg mb-6 relative z-10"
      />

      {/* 소개 텍스트 */}
      <h1 className="text-5xl md:text-5xl font-bold mb-2 relative z-10 mb-4">
        Daejin Kim
      </h1>
      <p className="text-lg md:text-2xl mt-4 mb-4 relative z-10">
        <span className="text-red-400">Fullstack</span> Developer |{" "}
        <span className="text-red-400">React</span> &{" "}
        <span className="text-red-400">.NET</span> &{" "}
        <span className="text-red-400">Node.js</span> Enthusiast
      </p>
      <p>
        Northern Alberta Institute of Technology, Digital Media & IT (Computer
        Software Development)
      </p>

      {/* 버튼 */}
      <div className="flex space-x-4 relative z-10 mt-5">
        <a href="#skills" className="btn btn-success">
          My Skills
        </a>

        <a href="#projects" className="btn btn-primary">
          My Projects
        </a>

        <a
          target="_blank"
          href="https://github.com/tedkim88/kdj-repo"
          className="btn btn-secondary"
        >
          Github Link
        </a>

        <a href="#contact" className="btn btn-secondary">
          Contact
        </a>
      </div>
    </section>
  );
}

export default Hero;
