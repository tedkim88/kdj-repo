console.log('js connected');

const apiKey = 'b6d56956eb404deb96c73522242411';
//base url depending on needs
const apiCurrentUrl = 'http://api.weatherapi.com/v1/current.json';
const apiHistoricalUrl = 'http://api.weatherapi.com/v1/history.json';
let langSelection = 'en';
let button = document.querySelector('#language-button');
document.querySelector('.dropdown-menu').addEventListener('click', (evt) => {
    if (evt.target.classList.contains('en')) {
        langSelection = 'en';
        button.innerText="English";
    }

    if (evt.target.classList.contains('ko')) {
        langSelection = 'ko';
        button.innerText="Korean";
    }
    console.log(langSelection);
})



//get current weather
function getWeather() {
    const city = document.getElementById('city').value;
    //if city is null
    if (!city) {
        alert('Please enter a city name');
        return; //stop function
    }

    const url = `${apiCurrentUrl}?key=${apiKey}&q=${city}&lang=${langSelection}`; //http query 

    fetch(url)
        .then(response => response.json())
        .then(data => {
            console.log(data);
            const temp = data.current.temp_c;
            const condition = data.current.condition.text;
            const humidity = data.current.humidity;
            const country = data.location.country;
            const localTime = data.location.localtime;
            const continent = data.location.tz_id;


            //inserting info into html tag
            document.getElementById('temp').innerText = `Temperature: ${temp}°C`;
            document.getElementById('current-condition').innerText = `Condition: ${condition}`;
            document.getElementById('humidity').innerText = `Humidity: ${humidity}%`;
            document.getElementById('country').innerText = `Country: ${country}`;
            document.getElementById('localTime').innerText = `Local Time: ${localTime}`;
            document.getElementById('continent').innerText = `Continent: ${continent}`;
        })
        .catch(error => {
            console.error('Error fetching weather data:', error);
            alert('Failed to fetch weather data. Please try again.');
        });
}


//get historical weather
function getHistoricalWeather() {
    const city = document.getElementById('historical-city').value; //get input data
    if (!city) {
        alert('Please enter a city name');
        return;
    }

    const date = document.getElementById('date').value;

    //valdation
    if (!date) {
        alert('Please choose the date');
        return;
    }

    const today = new Date();
    const selectedDate = new Date(date);
    //to prevent error from occuring when users choose future date by mistake for getting historical data.
    if (selectedDate > today) {
        alert('Please choose the day before today.')
        document.querySelector('#date').classList.add('is-invalid');
        return; //stop function
    }

    document.querySelector('#date').classList.remove('is-invalid');

    const url = `${apiHistoricalUrl}?key=${apiKey}&q=${city}&dt=${date}&lang=${langSelection}`; //http query, dt is required parameter according to the website for historical data.

    fetch(url)
        .then(response => response.json())
        .then(data => {
            console.log(data);
            const maxTemp = data.forecast.forecastday[0].day.maxtemp_c;
            const minTemp = data.forecast.forecastday[0].day.mintemp_c;
            const avgTemp = data.forecast.forecastday[0].day.avgtemp_c;
            const condition = data.forecast.forecastday[0].day.condition.text;

            console.log(condition);
            document.getElementById('chosen-date').innerText = `The historical weather of ${date}`;
            document.getElementById('maxTemp').innerText = `Max Temp: ${maxTemp}°C`;
            document.getElementById('minTemp').innerText = `Min Temp: ${minTemp}°C`;
            document.getElementById('avgTemp').innerText = `Average Temperature: ${avgTemp}°C`;
            document.getElementById('historical-condition').innerText = `Weather Condition: ${condition}`;
        })
        .catch(error => {
            console.error('Error fetching weather data:', error);
            alert('Failed to fetch weather data. Please try again.');
        });

}