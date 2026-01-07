const API = '/api/auth';

async function register(username, password){
  const res = await fetch(API + '/register', { method:'POST', headers:{'Content-Type':'application/json'}, body: JSON.stringify({ username, password }) });
  return res.status === 201 ? await res.json() : { error: (await res.json()).error ?? 'error' };
}

async function login(username, password){
  const res = await fetch(API + '/login', { method:'POST', headers:{'Content-Type':'application/json'}, body: JSON.stringify({ username, password }) });
  return res.ok ? await res.json() : {};
}