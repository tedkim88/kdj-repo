import Navbar from './components/Navbar'
import Hero from './components/Hero'
import Projects from './components/Projects'
import About from './components/About'
import Skills from './components/Skills'
import Contact from './components/Contact'
import ProblemSolving from './components/Problem-Solving'
// import Contact from './components/Contact'

function App() {
  return (
    <div>
      <Navbar />
       <Hero />
       <About />
       <Skills />
       <ProblemSolving />
       <Projects />
      <Contact /> 
      
    </div>
  )
}

export default App
