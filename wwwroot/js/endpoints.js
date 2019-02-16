let api = window.api = {};

const config = {
    protocol : 'https',
    server: 'localhost',
    port: '5001',
    prefixEndpoint: 'api',
    resolve(resource = '', controler = 'usedcar') {
        return `${this.protocol}://${this.server}:${this.port}/${this.prefixEndpoint}/${controler}/${resource}`
    }
};

(function() {
    this.getCars = () => {
        return axios.get(config.resolve(''))
    }

    this.getCar = id => {
        return axios.get(config.resolve(id))
    }

    this.postCar = car => {
        return axios.post(config.resolve(), car)
    }

    this.putCar = car => {
        return axios.put(config.resolve(), car)
    }

    this.deleteCar = id => {
        return axios.delete(config.resolve(id))
    }
}).apply(api)