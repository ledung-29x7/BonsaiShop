import axios from "../axios";

export const productPlamt = (plant) =>new Promise(async (resolve, reject) => {
    try {
    const response = await axios({
        url: `/${plant}`,
        method: "GET",
        headers: {
            Authorization: `Bearer ${window.sessionStorage.getItem("token")}`,
        },
    });
    resolve(response.data);
    } catch (error) {
    reject(error);
    }
});

export const homePlamt = () => new Promise(async (resolve, reject) => {
    try {
        const response = await axios({
            url:"",
            method: 'GET',
            headers: {

            }
        })
        resolve(response)
    } catch (error) {
        reject(error)
    }
})