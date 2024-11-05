import axios from "../axios";

export const login = (post) =>
    new Promise(async (resolve, reject) => {
      try {
        const response = await axios.post("api/login", post, {
          withCredentials: true,
        });
        resolve(response);
      } catch (error) {
        reject(error);
      }
    });