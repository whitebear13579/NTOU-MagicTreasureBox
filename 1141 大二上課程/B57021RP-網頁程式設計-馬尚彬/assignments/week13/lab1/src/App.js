import './App.css';
import Score from './Score';
import React, {useState} from "react";

function App() {
  const [scoreRed, setScoreRed] = useState(0);
  const [scoreBlue, setScoreBlue] = useState(0);
  let winner = null;

  if ( scoreRed >= 11 ){
    winner = '紅隊';
  }else if ( scoreBlue >= 11 ){
    winner = '藍隊';
  }

  return (
    <div className="App">
      <header className="App-header">
        {winner && (
          <div style={{ 
            fontSize: "40px", 
            marginBottom: "20px", 
            color: winner === '紅隊' ? 'red' : 'blue',
            fontWeight: "bold"
          }}>
            {winner}獲勝
          </div>
        )}
        <div style={{ display: "flex" }}>
          <Score 
            color="blue"
            score={scoreBlue}
            onClick={() => setScoreBlue(scoreBlue + 1)}
          />
          <Score 
            color="red"
            score={scoreRed}
            onClick={() => setScoreRed(scoreRed + 1)}
          />
        </div>
      </header>
    </div>
  );
}

export default App;
