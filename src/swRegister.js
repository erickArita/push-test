// TODOO: recibir el authUserId
export default function LocalServiceWorkerRegister() {
  const swPath = `${process.env.PUBLIC_URL}/service-workerr.js`;
  if ('serviceWorker' in navigator && process.env.NODE_ENV !== 'production') {
    // eslint-disable-next-line no-restricted-globals
    self.addEventListener('load', async function () {
      const sw = await navigator.serviceWorker.register(swPath)

      const subscription = await sw.pushManager.subscribe({
        userVisibleOnly: true,
        applicationServerKey: urlBase64ToUint8Array("BI8i-gq-JMMmkzDUdEemTvGNOF7v2YlQgzxo0UcCLLXkgYCWWdtfRQuM_U7oy6gWFF2zI5HjZkdlVMs9xxiJjvM")
      })

      const s = subscription.toJSON()
      // TODO:mandar el user id para asociarlo con un subscripción de notoficación
      await subscribe({ user: 'Erick', ...s })
    });
  }

}


const subscribe = async (subscription) => {
  // TODO:Colocar la url del backend
  await fetch('https://localhost:7272/subscribe', {
    method: 'POST',
    body: JSON.stringify(subscription),
    headers: {
      'Content-type': 'application/json',
      'Access-Control-Allow-Origin': 'no-cors'
    }
  })
}

function urlBase64ToUint8Array(base64String) {
  const padding = "=".repeat((4 - (base64String.length % 4)) % 4);
  const base64 = (base64String + padding).replace(/-/g, "+").replace(/_/g, "/");

  const rawData = window.atob(base64);
  const outputArray = new Uint8Array(rawData.length);

  for (let i = 0; i < rawData.length; ++i) {
    outputArray[i] = rawData.charCodeAt(i);
  }
  return outputArray;
}