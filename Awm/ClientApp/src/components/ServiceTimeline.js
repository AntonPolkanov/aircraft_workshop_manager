import React, {Component} from 'react';
import moment from 'moment';
import Timeline, {
  TimelineHeaders,
  SidebarHeader,
  DateHeader
} from "react-calendar-timeline/lib";
import generateFakeData from "./generateFakeData";


const keys = {
  groupIdKey: "id",
  groupTitleKey: "title",
  groupRightTitleKey: "rightTitle",
  itemIdKey: "id",
  itemTitleKey: "title",
  itemDivTitleKey: "title",
  itemGroupKey: "group",
  itemTimeStartKey: "start",
  itemTimeEndKey: "end",
  groupLabelKey: "title"
};

export class ServiceTimeline extends Component {
  constructor(props) {
    super(props);
    this.state = {
      groups: [],
      items: [],
      defaultTimeStart: '',
      defaultTimeEnd: '',
      loading: true
    };
  }

  async componentDidMount() {
    const {groups, items} = await generateFakeData(10);
    const defaultTimeStart = moment()
      .startOf("day")
      .toDate();
    const defaultTimeEnd = moment()
      .startOf("day")
      .add(1, "day")
      .toDate();
    this.setState({
      groups: groups,
      items: items,
      defaultTimeStart: defaultTimeStart,
      defaultTimeEnd: defaultTimeEnd,
      loading: false
    });
  }
  
  render() {
    const {groups, items, defaultTimeStart, defaultTimeEnd} = this.state;

    return (
      <>
        {!this.state.loading &&
        <Timeline
              groups={groups}
              items={items}
              keys={keys}
              sidebarContent={<div>Above The Left</div>}
              itemsSorted
              itemTouchSendsClick={false}
              stackItems
              itemHeightRatio={0.75}
              showCursorLine
              canMove={true}
              canResize={true}
              defaultTimeStart={defaultTimeStart}
              defaultTimeEnd={defaultTimeEnd}
            >
              <TimelineHeaders className="sticky" style={{backgroundColor: '#3d64a6'}}>
                <SidebarHeader>
                  {({ getRootProps }) => {
                    return <div {...getRootProps()}><font color="white" style={{marginTop:30, marginLeft:20}}>Reg.number</font></div>;
                  }}
                </SidebarHeader>
                <DateHeader unit="primaryHeader"/>
                <DateHeader/>
              </TimelineHeaders>
        </Timeline>}
      </>
    );
  }
}