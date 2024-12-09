import axios from "../axios";

export const productPlamt = (id) =>new Promise(async (resolve, reject) => {
    try {
    const response = await axios({
        url: `/Plant/${id}`,
        method: "GET",
        
    });
    resolve(response.data);
    } catch (error) {
    reject(error);
    }
});

export const ImagePlant= (id) =>new Promise(async (resolve, reject) => {
    try {
    const response = await axios({
        url: `/plant/${id}/images`,
        method: "GET",
        
    });
    resolve(response);
    } catch (error) {
    reject(error);
    }
});

export const homePlamt = () => new Promise(async (resolve, reject) => {
    try {
        const response = await axios({
            url:"/Plant",
            method: 'GET',
            
        })
        resolve(response)
    } catch (error) {
        reject(error)
    }
})
export const order = (data) => new Promise(async (resolve, reject) => {
    try {
        const response = await axios({
            url:"/orders",
            method: 'Post',
            data: data
        })
        resolve(response)
    } catch (error) {
        reject(error)
    }
})

