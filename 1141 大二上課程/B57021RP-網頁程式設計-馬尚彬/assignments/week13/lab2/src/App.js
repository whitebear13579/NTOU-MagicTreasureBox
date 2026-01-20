import './App.css';
import Axios from 'axios';
import { useEffect, useState } from 'react';

function App() {

  const [catFact, setCatFact] = useState("")
  useEffect(()=>{
    Axios.get("https://catfact.ninja/fact").then((res)=>{
      console.log(res.data);
      setCatFact(res.data.fact);
    });
  },[]);

  return (
    <div className="App">
      <header className="App-header">
        {catFact}
      </header>
    </div>
  );
}

export default App;
