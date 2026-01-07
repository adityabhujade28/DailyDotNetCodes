const AUTH_API = '/api/auth';

async function register(username, password){
  const res = await fetch(AUTH_API + '/register', { method:'POST', headers:{'Content-Type':'application/json'}, body: JSON.stringify({ Username: username, Password: password }) });
  if (res.status === 201) return await res.json();
  const body = await res.json();
  return { error: body.error ?? 'error' };
}

async function login(username, password){
  const res = await fetch(AUTH_API + '/login', { method:'POST', headers:{'Content-Type':'application/json'}, body: JSON.stringify({ Username: username, Password: password }) });
  if (res.ok) return await res.json();
  return { error: 'invalid' };
}

async function getUser(userId){
  const res = await fetch(`/api/users/${userId}`);
  if (!res.ok) return null;
  return await res.json();
}