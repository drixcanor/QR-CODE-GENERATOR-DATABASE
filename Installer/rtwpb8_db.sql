-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 31, 2024 at 09:13 PM
-- Server version: 10.4.28-MariaDB
-- PHP Version: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `rtwpb8_db`
--

-- --------------------------------------------------------

--
-- Table structure for table `tbl_check`
--

CREATE TABLE `tbl_check` (
  `check_id` int(11) NOT NULL,
  `check_date` varchar(255) NOT NULL,
  `check_type` varchar(255) NOT NULL,
  `check_particular` varchar(255) NOT NULL,
  `check_desired` varchar(255) NOT NULL,
  `check_taken` varchar(255) NOT NULL,
  `check_datenow` varchar(255) NOT NULL,
  `check_timeout` varchar(255) NOT NULL,
  `check_remark` varchar(255) NOT NULL,
  `check_qr` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tbl_check`
--

INSERT INTO `tbl_check` (`check_id`, `check_date`, `check_type`, `check_particular`, `check_desired`, `check_taken`, `check_datenow`, `check_timeout`, `check_remark`, `check_qr`) VALUES
(7, 'June 01, 2024', 'COMMUNICATIONS', '', 'FOR ENDORSEMENT TO ORD', 'RETURNED', 'June 01, 2024', '02:24 am', '', 'C:\\Users\\ESCANOR\\Pictures\\QRCode_20240601022412135.png'),
(8, 'June 01, 2024', 'FUEL WITHDRAWAL', '', 'FOR REVIEW', 'RETURNED', 'June 01, 2024', '02:24 am', '', 'C:\\Users\\ESCANOR\\Pictures\\QRCode_20240601022420938.png'),
(9, 'June 01, 2024', 'COMMUNICATIONS', 'ss', 'FOR ENDORSEMENT TO ORD', 'REVIEWED', 'June 01, 2024', '02:56 am', '', 'C:\\Users\\ESCANOR\\Pictures\\QRCode_20240601025621639.png');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `tbl_check`
--
ALTER TABLE `tbl_check`
  ADD PRIMARY KEY (`check_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `tbl_check`
--
ALTER TABLE `tbl_check`
  MODIFY `check_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
