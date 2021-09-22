﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Xunit;

namespace CS.Changelog.Tests
{

	/// <summary>
	/// Tests <see cref="ChangelogService"/>
	/// </summary>
	public class ServiceTests
	{
		/// <summary>
		/// Changelogs the unification test.
		/// </summary>
		[Fact]
		public void ChangelogUnificationTest()
		{
			//Prepare
			var changelogs = new List<string>() {
				  "TestFiles/changelog1.json"
				, "TestFiles/changelog2.json" };
			var hideIssueTrackerInfo = false;
			var hideCommitDetails = false;
			var ignoredCategories = new List<string>();


			var files = changelogs.Select(x => new FileInfo(x)).ToArray();

			//Test
			var response = ChangelogService.GetChangelogs(files, hideIssueTrackerInfo, hideCommitDetails, ignoredCategories);

			//Assert
			Console.WriteLine(JsonConvert.SerializeObject(response));

			var expectedChangeLogs = 2;
			Assert.False(response.Failure.Any());
			Assert.Equal(expectedChangeLogs, response.Success.Count());
		}

		/// <summary>
		/// Changelogs the unification with empty test.
		/// </summary>
		[Fact]
		public void ChangelogUnificationWithEmptyTest()
		{
			//Prepare
			var changelogs = new List<string>() {
				  "TestFiles/changelog1.json"
				, "TestFiles/EmptyChangelog.json" };
			var hideIssueTrackerInfo = false;
			var hideCommitDetails = false;
			var ignoredCategories = new List<string>();

			var files = changelogs.Select(x => new FileInfo(x)).ToArray();

			//Test
			var response = ChangelogService.GetChangelogs(files, hideIssueTrackerInfo, hideCommitDetails, ignoredCategories);

			//Assert
			Console.WriteLine(JsonConvert.SerializeObject(response));

			var expectedChangeLogs = 1;
			var expectedFailures = 1;
			Assert.True(response.Failure.Any());
			Assert.Equal(expectedFailures, response.Failure.Count());
			Assert.Equal(expectedChangeLogs, response.Success.Count());
		}


		/// <summary>
		/// Nons the existent changelogs unification test.
		/// </summary>
		[Fact]
		public void NonExistentChangelogsUnificationTest()
		{
			//Prepare
			var changelogs = new List<string>() {
				  "NonExistent.json"
				, "Makeup.json" };
			var hideIssueTrackerInfo = false;
			var hideCommitDetails = false;
			var ignoredCategories = new List<string>();

			var files = changelogs.Select(x => new FileInfo(x)).ToArray();

			//Test
			var response = ChangelogService.GetChangelogs(files, hideIssueTrackerInfo, hideCommitDetails, ignoredCategories);

			//Assert
			Console.WriteLine(JsonConvert.SerializeObject(response));


			var expectedChangeLogs = 0;
			var expectedFailures = 2;
			Assert.True(response.Failure.Any());
			Assert.Equal(expectedFailures, response.Failure.Count());
			Assert.Equal(expectedChangeLogs, response.Success.Count());
		}
	}
}
