import React, { useState } from "react";

function Navbar() {
  const [open, setOpen] = useState(false);

  return (
    <nav className="bg-base-100 shadow-md sticky top-0 z-50">
      <div className="max-w-6xl mx-auto px-4 flex justify-between items-center h-16">
        {/* 로고 / 사이트 이름 */}
        <a href="#" className="text-2xl  font-bold hover:text-indigo-500 transition-colors">
          Daejin Kim's Portfolio
        </a>

        {/* 데스크탑 메뉴 */}
        <ul className="hidden md:flex  space-x-6">
          <li><a href="#about" className="hover:text-indigo-500 transition-colors">About</a></li>
          <li><a href="#skills" className="hover:text-indigo-500 transition-colors">Skills</a></li>
          <li><a href="#projects" className="hover:text-indigo-500 transition-colors">Projects</a></li>
          <li><a href="#contact" className="hover:text-indigo-500 transition-colors">Contact</a></li>
        </ul>

        {/* 모바일 햄버거 버튼 */}
        <button
          className="md:hidden btn btn-ghost"
          onClick={() => setOpen(!open)}
        >
          ☰
        </button>
      </div>

      {/* 모바일 메뉴 */}
      {open && (
        <ul className="md:hidden bg-base-100 px-4 pb-4 space-y-2">
          <li><a href="#about" className="block py-2 hover:text-indigo-500 transition-colors">About</a></li>
          <li><a href="#skills" className="block py-2 hover:text-indigo-500 transition-colors">Skills</a></li>
          <li><a href="#projects" className="block py-2 hover:text-indigo-500 transition-colors">Projects</a></li>
          <li><a href="#contact" className="block py-2 hover:text-indigo-500 transition-colors">Contact</a></li>
        </ul>
      )}
    </nav>
  );
}

export default Navbar;
