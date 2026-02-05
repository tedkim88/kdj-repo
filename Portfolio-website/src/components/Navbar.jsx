"use client";
import React, { useState } from "react";
import { useEffect } from "react";
function Navbar() {
  const [open, setOpen] = useState(false);
  const [scrolled, setScrolled] = useState(false);

  useEffect(() => {
    window.addEventListener("scroll", () => {
      if (window.scrollY > 300) {
        setScrolled(true);
      } else {
        setScrolled(false);
      }
    });
  }, []);
  return (
    <nav
      className={`bg-gradient-to-b py-3 from-black ${scrolled ? "via-blue-300" : "via-gray-200"} to-black text-black  shadow-md sticky top-0 z-50`}
    >
      <div className="max-w-6xl mx-auto px-4 flex justify-between items-center h-16">
        {/* 로고 / 사이트 이름 */}
        <a
          href="#"
          className="text-2xl font-bold hover:text-indigo-700  transition-colors"
        >
          Daejin Kim's Portfolio
        </a>

        {/* 데스크탑 메뉴 */}
        <ul className="hidden md:flex space-x-6">
          <li>
            <a
              href="#about"
              className="text-black btn-home nav-link hover:text-indigo-700 hover:text-lg transition-colors"
            >
              About
            </a>
          </li>
          <li>
            <a
              href="#skills"
              className="text-black btn-home nav-link hover:text-indigo-700 hover:text-lg transition-colors"
            >
              Skills
            </a>
          </li>
          <li>
            <a
              href="#problem-solving"
              className="text-black btn-home nav-link hover:text-indigo-700 hover:text-lg transition-colors"
            >
              Problem Solving
            </a>
          </li>
          <li>
            <a
              href="#projects"
              className="text-black btn-home nav-link hover:text-indigo-700 hover:text-lg transition-colors"
            >
              Projects
            </a>
          </li>
          <li>
            <a
              href="#contact"
              className="text-black btn-home nav-link hover:text-indigo-700 hover:text-lg transition-colors"
            >
              Contact
            </a>
          </li>
        </ul>

        {/* 모바일 햄버거 버튼 */}
        <button
          className="md:hidden btn bg-gray-900 text-yellow-400 hover:text-red-600 px-4 py-2 rounded "
          onClick={() => setOpen(!open)}
        >
          ☰
        </button>
      </div>

      {/* 모바일 메뉴 */}
      {open && (
        <ul className="md:hidden bg-base-100 text-white px-4 pb-4 space-y-2 mobile-menu">
          <li>
            <a
              href="#about"
              className="block py-2 hover:text-yellow-500 transition-colors"
            >
              About
            </a>
          </li>
          <li>
            <a
              href="#skills"
              className="block py-2 hover:text-yellow-500 transition-colors"
            >
              Skills
            </a>
          </li>
          <li>
            <a
              href="#problem-solving"
              className="block py-2 hover:text-yellow-500 transition-colors"
            >
              Problem Solving
            </a>
          </li>
          <li>
            <a
              href="#projects"
              className="block py-2 hover:text-yellow-500 transition-colors"
            >
              Projects
            </a>
          </li>
          <li>
            <a
              href="#contact"
              className="block py-2 hover:text-yellow-500 transition-colors"
            >
              Contact
            </a>
          </li>
        </ul>
      )}
    </nav>
  );
}

export default Navbar;
