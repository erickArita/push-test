self.addEventListener('push', e => {

  const data = (e.data.json());

  self.registration.showNotification(data.Title, {
    body: data.Body + self.navigator.platform + self.navigator.userAgent,
    icon: '/logo512.png'
  })
})