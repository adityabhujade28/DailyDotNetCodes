const GAME_API = '/api/game';
let currentUserId = localStorage.getItem('token');
let currentGameId = null;

async function createGame() {
  const playerXId = currentUserId;
  const playerOId = currentUserId; 
  const res = await fetch(GAME_API + '/create', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ PlayerXId: playerXId, PlayerOId: playerOId })
  });
  return res.ok ? await res.json() : null;
}

async function getGame(id) {
  const res = await fetch(GAME_API + '/' + id);
  return res.ok ? await res.json() : null;
}

async function makeMove(gameId, row, col) {
  const res = await fetch(`${GAME_API}/${gameId}/move`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ PlayerId: currentUserId, Row: row, Col: col })
  });
  return res.ok ? await res.json() : { error: 'Move failed' };
}

function renderBoard(board, isFinished) {
  const boardEl = document.getElementById('board');
  boardEl.innerHTML = '';
  
  for (let r = 0; r < 3; r++) {
    for (let c = 0; c < 3; c++) {
      const cell = document.createElement('div');
      cell.className = 'cell';
      const val = board[r][c];
      
      if (val === 1) {
        cell.innerText = 'X';
        cell.classList.add('x');
      } else if (val === 2) {
        cell.innerText = 'O';
        cell.classList.add('o');
      }
      
      if (!isFinished) {
        cell.addEventListener('click', async () => {
          if (!currentGameId) return;
          if (val !== 0) return
          
          const result = await makeMove(currentGameId, r, c);
          if (result.error) {
            console.log('Move error:', result.error);
            return;
          }
          await refreshGame();
        });
      }
      
      boardEl.appendChild(cell);
    }
  }
}

function updateTurnIndicator(turn, isFinished) {
  const turnEl = document.getElementById('currentTurn');
  const indicatorEl = document.getElementById('turnIndicator');
  
  if (isFinished) {
    indicatorEl.style.display = 'none';
  } else {
    indicatorEl.style.display = 'block';
    if (turn === 1) {
      turnEl.innerText = "X's Turn";
      turnEl.className = 'x-turn';
    } else {
      turnEl.innerText = "O's Turn";
      turnEl.className = 'o-turn';
    }
  }
}

function updateStatus(isFinished, winner) {
  const statusEl = document.getElementById('statusDisplay');
  const statusText = document.getElementById('statusText');
  
  if (!isFinished) {
    statusEl.style.display = 'none';
    return;
  }
  
  statusEl.style.display = 'block';
  statusEl.className = 'status-display';
  
  if (winner === 1) {
    statusText.innerText = 'X Wins!';
    statusEl.classList.add('winner-x');
  } else if (winner === 2) {
    statusText.innerText = 'O Wins!';
    statusEl.classList.add('winner-o');
  } else {
    statusText.innerText = "It's a Draw!";
    statusEl.classList.add('draw');
  }
}

async function refreshGame() {
  if (!currentGameId) return;
  
  const g = await getGame(currentGameId);
  if (!g) {
    console.log('Game not found');
    return;
  }
  
  // Handle both PascalCase and camelCase
  const board = g.Board ?? g.board;
  const isFinished = g.IsFinished ?? g.isFinished;
  const winner = g.Winner ?? g.winner;
  const turn = g.Turn ?? g.turn;
  
  renderBoard(board, isFinished);
  updateTurnIndicator(turn, isFinished);
  updateStatus(isFinished, winner);
}

async function startNewGame() {
  const result = await createGame();
  if (!result) {
    alert('Failed to create game');
    return;
  }
  
  currentGameId = result.id ?? result.Id;
  
  // Reset UI for new game
  document.getElementById('turnIndicator').style.display = 'block';
  document.getElementById('statusDisplay').style.display = 'none';
  
  await refreshGame();
}

async function initUI() {
  const token = localStorage.getItem('token');
  if (!token) {
    document.getElementById('authSection').style.display = 'block';
    return;
  }
  
  currentUserId = token;
  document.getElementById('authSection').style.display = 'none';
  document.getElementById('app').style.display = 'block';
  
  // Try to load existing active game or create new one
  try {
    const res = await fetch(`/api/game/user/${currentUserId}`);
    if (res.ok) {
      const games = await res.json();
      const active = games.find(g => !(g.IsFinished ?? g.isFinished));
      
      if (active) {
        currentGameId = active.Id ?? active.id;
        await refreshGame();
      } else {
        await startNewGame();
      }
    }
  } catch (e) {
    console.warn('Failed to fetch user games', e);
    await startNewGame();
  }
  
  // New Game button
  document.getElementById('newGameBtn').addEventListener('click', startNewGame);
  
  // Logout button
  document.getElementById('logoutBtn').addEventListener('click', logout);
}

async function logout() {
  // Delete unfinished games before logout
  if (currentUserId) {
    await fetch(`/api/game/user/${currentUserId}/unfinished`, { method: 'DELETE' });
  }
  localStorage.removeItem('token');
  currentUserId = null;
  currentGameId = null;
  window.location.href = '/';
}

window.addEventListener('DOMContentLoaded', initUI);
