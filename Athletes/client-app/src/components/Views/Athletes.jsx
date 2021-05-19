
import React, {useEffect, useState} from 'react'
import { getAthletes } from '../../network';

export default function Athletes() {
  const [athletes, setAthletes] = useState([])

  useEffect(() => {
    (async () => {
      const response = await getAthletes();
      setAthletes(response.data)
      console.log("get all athletes", response.data)
      
    })();
  }, []);

    return (
        <div>
            <h3>Bridge Athletics</h3>
            <ul>
              {
                athletes?.map(a => <li key={a.Id}> {a.FirstName}</li>)
              }
            </ul>
        </div>
    )
}
