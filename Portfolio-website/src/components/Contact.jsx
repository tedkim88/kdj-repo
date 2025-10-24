import React from "react";
import Particles from "react-tsparticles";
import { loadFull } from "tsparticles";

function Contact() {
  const particlesInit = async (main) => {
    await loadFull(main);
  };

  return (
    <section
      id="contact"
      className="relative min-h-screen px-8 py-16 flex flex-col items-center justify-center text-white overflow-hidden bg-gradient-to-b from-gray-800 via-indigo-900 to-black"
    >
      {/* 배경 입자 */}
      <Particles
        id="tsparticles-contact"
        init={particlesInit}
        options={{
          fullScreen: { enable: false },
          background: { color: { value: "transparent" } },
          particles: {
            number: { value: 25 },
            color: { value: ["#ffffff", "#a78bfa", "#f472b6"] },
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
            },
          },
        }}
        className="absolute inset-0 z-0"
      />

      {/* 섹션 콘텐츠 */}
      <h2 className="text-4xl font-bold mb-8 relative z-10 text-center">
        Contact Me
      </h2>
      <p className="mb-12 text-center relative z-10">
        Feel free to reach out for collaborations or questions.
      </p>

      <div className="flex flex-col md:items-center md:justify-center gap-4 md:gap-6 relative z-10 text-center">
        {/* 텍스트 영역 */}
        <div className="flex flex-col md:flex-row md:items-center gap-2 md:gap-4">
          <p className="px-4 py-2 bg-gray-800 rounded-lg">
            Email: tedkdj88@gmail.com
          </p>
          <p className="px-4 py-2 bg-gray-800 rounded-lg">
            Phone: +1 236-597-8576
          </p>
        </div>

        {/* 버튼 영역 */}

        <div className="flex flex-col md:flex-row gap-2 md:gap-4 mt-4 md:mt-0">
          <a
            href="https://github.com/tedkim88/kdj-repo"
            target="_blank"
            rel="noopener noreferrer"
            className="btn btn-home btn-secondary px-6 py-3"
          >
            GitHub
          </a>
          <a
            href="https://www.linkedin.com/in/daejin-kim-2330b0312/"
            target="_blank"
            rel="noopener noreferrer"
            className="btn btn-home btn-accent px-6 py-3"
          >
            LinkedIn
          </a>
        </div>
      </div>
    </section>
  );
}

export default Contact;
