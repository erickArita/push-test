/* eslint-disable no-undef */
/* eslint-disable no-restricted-globals */
self.addEventListener('push', e => {

  const data = e.data.json();

  self.registration.showNotification(data.title, { icon: '/logo512.png', ...data })
})

self.addEventListener('notificationclick', (event) => {

  event.notification.close();

  clients.openWindow(event.notification.data.url, '_blank');

});