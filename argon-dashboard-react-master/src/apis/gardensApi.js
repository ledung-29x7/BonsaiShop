import axios from "../axios";

export const plantGarden = (plant) =>
    new Promise(async (resolve, reject) => {
      try {
        const response = await axios({
          url: `plant/`,
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