import React from 'react';
import ReactDOM from 'react-dom/client';

import App from './App';
import './index.css';
import LocalServiceWorkerRegister from './swRegister';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
);
// TODO: registrar el servise worker despues de loguear al usuario
LocalServiceWorkerRegister();