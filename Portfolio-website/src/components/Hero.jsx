import React from "react";
import Particles from "react-tsparticles";
import { loadFull } from "tsparticles";

function Hero() {
  const particlesInit = async (main) => {
    await loadFull(main); // tsparticles 엔진 로드
  };

  return (
    <section className="relative min-h-[calc(100vh-64px)] flex flex-col items-center justify-center text-white text-center px-4 overflow-hidden bg-black">
      {/* 배경 입자 */}
      <Particles
        id="tsparticles"
        init={particlesInit}
        options={{
          fullScreen: { enable: false },
          background: { color: { value: "transparent" } },
          particles: {
            number: { value: 12 },
            color: {
              value: ["#48E8DD", "#E8E548", "#e0e0e0", "#fafafa"],
            },

            shape: { type: ["circle", "triangle"] },
            size: { value: { min: 1, max: 2 } },
            move: { enable: true, speed: 2, outModes: "out" },
            links: {
              enable: false,
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

      <div className="rounded-full inline-block relative">
        <img
          src="/daejin.jpg"
          alt="My Photo"
          className="w-64 h-64 md:w-80 md:h-80 rounded-full mb-6 relative z-10
               shadow-[0_0_50px_25px_rgba(96,165,250,0.35),0_0_100px_50px_rgba(96,165,250,0.2)]"
        />
      </div>

      {/* 소개 텍스트 */}
      <h1 className="text-5xl h-15 font-bold bg-gradient-to-br mb-4 from-cyan-100 via-cyan-400 to-cyan-700 bg-clip-text text-transparent">
        Daejin Kim
      </h1>

      <p className="text-lg md:text-2xl mt-4 mb-4 relative z-10">
        <span className="text-yellow-400">Fullstack</span> Developer |{" "}
        <span className="text-yellow-400">React</span> &{" "}
        <span className="text-yellow-400">.NET</span> &{" "}
        <span className="text-yellow-400">Node.js</span> Enthusiast
      </p>
      <p>
        <span className="text-yellow-400">(NAIT)</span> Northern Alberta
        Institute of Technology, Digital Media & IT{" "}
      </p>
      <p className="text-yellow-400">(Computer Software Development)</p>

      {/* 버튼 */}
      <div className="flex flex-col md:flex-row gap-4 relative z-10 mt-5">
        <div className="flex space-x-4">
          <a href="#skills" className="btn btn-success btn-home w-30">
            My Skills
          </a>

          <a href="#projects" className="btn btn-primary btn-home w-30">
            My Projects
          </a>
        </div>
        <div className="flex space-x-4">
          <a
            target="_blank"
            href="https://github.com/tedkim88/kdj-repo"
            className="btn btn-info w-30 btn-home"
          >
            Github Link
          </a>

          <a href="#contact" className="btn btn-secondary btn-home w-30">
            Contact
          </a>
        </div>
      </div>
    </section>
  );
}

export default Hero;
