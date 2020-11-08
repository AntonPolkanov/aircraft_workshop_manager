import faker from 'faker'
import randomColor from 'randomcolor'
import moment from 'moment'
import axios from 'axios'

export default async function (groupCount = 30, itemCount = 100, daysInPast = 30) {
  let randomSeed = Math.floor(Math.random() * 1000)
  const response = await axios.get(`${process.env.REACT_APP_API_URL}/aircrafts`);
  const aircrafts = await response.data;
  let groups = []
  for (let i = 0; i < aircrafts.length; i++) {
    groups.push({
      id: aircrafts[i].id + '',
      title: aircrafts[i].registrationNumber,
      rightTitle: aircrafts[i].engine,
      bgColor: randomColor({ luminosity: 'light', seed: randomSeed + i })
    })
  }

  let items = []
  let id = 0;
  for (let i = 0; i < aircrafts.length; i++) {
    for (let j = 0; j < aircrafts[i]?.workedOn?.length; j++) {
      let worker = aircrafts[i].workedOn[j];

      const startValue = moment(worker.startDate, "YYYY-MM-DD").valueOf();
      const endValue = moment(worker.endDate, "YYYY-MM-DD").valueOf();
      items.push({
        id: id + '',
        group: aircrafts[i].id + '',
        title: worker.name ?? "Planned",
        start: startValue,
        end: endValue,
        itemProps: {
          'data-tip': worker.name
        }
      })
      id++;
    }

  }

  items = items.sort((a, b) => b - a)

  return { groups, items }
}
