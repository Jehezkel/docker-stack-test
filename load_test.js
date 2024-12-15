import http from 'k6/http';
import { sleep } from 'k6';

export let options = {
  vus: 400,         // Number of virtual users
  duration: '30s', // Duration of the test
};

export default function () {
  let res = http.get('http://u239029.webh.me/api/weatherforecast'); // Replace with your target URL
  // Optionally, add assertions to check response status
  if (res.status !== 200) {
    console.error(`Request failed. Status: ${res.status}`);
  }
  sleep(1); // Simulate user wait time (in seconds) between requests
}

