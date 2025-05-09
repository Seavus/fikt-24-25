export const barChartData = {
  labels: ['Product A', 'Product B', 'Product C', 'Product D', 'Product E'],
  datasets: [
    {
      label: 'Sales',
      data: [100, 200, 300, 300, 400],
      backgroundColor: ['#4caf50', '#2196f3', '#ff9800'],
    },
  ],
};

export const lineChartData = {
  labels: ['January', 'February', 'March', 'April', 'May'],
  datasets: [
    {
      label: 'Line Dataset',
      data: [10, 20, 30, 20, 40],
      borderColor: 'rgb(75, 192, 192)',
      fill: false,
      tension: 0.1,
    },
  ],
};

export const doughnutChartData = {
  labels: ['Category A', 'Category B', 'Category C'],
  datasets: [
    {
      label: 'Categories',
      data: [50, 30, 20],
      backgroundColor: ['#ff6384', '#36a2eb', '#ffce56'],
    },
  ],
};
