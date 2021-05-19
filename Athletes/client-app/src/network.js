import axios from 'axios'

// ============ ATHLETES ============

export const getAthletes = async () => {
    try {
        const result = await axios.get(`https://localhost:44353/api/athleteapi`, {
          headers: {
            "Access-Control-Allow-Origin": "*"
          }
        })
        console.log("getting atletes - in network", result)
      return result
    } catch (error) {
      console.log(error)
    }
}



// ============ COACHES ============